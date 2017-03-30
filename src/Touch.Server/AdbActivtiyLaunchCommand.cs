using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Touch.Server.Android.Extensions;

namespace Touch.Server.Android
{
	class AdbActivtiyLaunchCommand
    {
        private const string AdbExeName = "adb.exe";
		private const string AdbCmdName = "adb";

        public string AdbExeFile { get; set; }
        public string Activity { get; set; }
        public int ExitCode { get; set; }
        public IList<string> AdbParams { get; set; }

        public AdbActivtiyLaunchCommand(string activity, string adbExeFile)
        {
            this.Activity = activity;
            this.AdbExeFile = this.PrepareAdbExeFilePath(adbExeFile);
            this.ExitCode = -1;
        }

        public void Execute()
        {
            ServerRunner.LogMessage("Executing adb command\n Activity : {0}\n AdbExeFilePath : {1}", this.Activity, this.AdbExeFile);

			var info = new ProcessStartInfo
			{
				FileName = this.AdbExeFile,
				Arguments = this.BuildActivitylaunchCommand()
			};

			if (Environment.OSVersion.Platform.IsMacOSX())
				info.UseShellExecute = false; // Mac OSX and Unix
			else
				info.UseShellExecute = true; // for Windows machines

            ServerRunner.LogMessage($"arguments for adb: {info.Arguments}");

			using (var proc = Process.Start(info))
            {
                proc.WaitForExit();

                ServerRunner.LogMessage("Finished with adb command (return code : {0})", proc.ExitCode);
                this.ExitCode = proc.ExitCode;
            }
        }

        private string BuildActivitylaunchCommand()
        {
            var command = "shell am start -a android.intent.action.MAIN -n " + this.Activity;

            if (AdbParams != null && AdbParams.Any())
            {
                foreach (var adbParam in AdbParams)
                    command += $" {adbParam}";
            }

            return command;
        }

        private string PrepareAdbExeFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return AdbExeName;
            }

			if (path.EndsWith(AdbExeName, StringComparison.InvariantCulture))
            {
                return path;
            }

			return Path.Combine(path, Environment.OSVersion.Platform.IsMacOSX() ? AdbCmdName : AdbExeName);
        }
    }
}

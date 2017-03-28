using System.Collections.Generic;
using System.Linq;

namespace Touch.Server.Android
{
    using System.Diagnostics;

    class AdbActivtiyLaunchCommand
    {
        private const string AdbExeName = "adb.exe";

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
                UseShellExecute = true,
                Arguments = this.BuildActivitylaunchCommand(),
            };

            ServerRunner.LogMessage($"arguments for adb: {info.Arguments}");

            using (var proc = new Process())
            {
                proc.StartInfo = info;

                proc.Start();
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

            if (path.Contains("adb"))
            {
                return path;
            }

            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }

            return path + AdbExeName;
        }
    }
}

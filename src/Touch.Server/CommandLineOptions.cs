using System.Collections.Generic;
using System.Linq;

using CommandLine;
using CommandLine.Text;

namespace Touch.Server.Android
{
	class CommandLineOptions
	{
		[Option("ip", HelpText = "IP address to listen (default: Any)")]
		public string IpAddress { get; set; }

		[Option("port", HelpText = "TCP port to listen (default: Any)")]
		public string Port { get; set; }

		[Option("logpath", HelpText = "Path to save the log files (default: .)")]
		public string LogFilePath { get; set; }

		[Option("logfile", HelpText = "Filename to save the log to (default: automatically generated)")]
		public string LogFileName { get; set; }

		[Option("autoexit", DefaultValue = false, HelpText = "Exit the server once a test run has completed (default: false)")]
		public bool AutoExit { get; set; }

		[Option("activity", HelpText = "Fully qualified activity name that will be launched after server starts.(exmaple : " +
									   "package_name/namespace.MainActivity)")]
		public string Activity { get; set; }

		[Option("adbpath", HelpText = "Path to adb.exe (default: will use adb.exe from environment)")]
		public string AdbPath { get; set; }

		[Option('v', null, HelpText = "Print details during execution.")]
		public bool Verbose { get; set; }

		[OptionList('a', "adbargs", Separator = ':', HelpText = "Arguments apssed to the adb.exe")]
		public IList<string> AdbParams { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this);
		}

		public override string ToString()
		{
			return $"{nameof(CommandLineOptions)} [{nameof(IpAddress)} = {IpAddress}]\r\n" +
				$"[{nameof(Port)} = {Port}]\r\n" +
				$"[{nameof(LogFilePath)} = {LogFilePath}]\r\n" +
				$"[{nameof(LogFileName)} = {LogFileName}]\r\n" +
				$"[{nameof(AutoExit)} = {AutoExit.ToString()}]\r\n" +
				$"[{nameof(Activity)} = {Activity}]\r\n" +
				$"[{nameof(AdbPath)} = {AdbPath}]\r\n" +
				$"[{nameof(Verbose)} = {Verbose.ToString()}]\r\n" +
				$"[{nameof(AdbParams)} = {AdbParams == null ? string.Empty : AdbParams.Aggregate(string.Empty, (text, arg) => string.IsNullOrWhiteSpace(text) ? arg : $"{text} | {arg}")}]\r\n";
		}
	}
}

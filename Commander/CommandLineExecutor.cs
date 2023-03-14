using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Commander
{
    public class CommandLineExecutor
    {
        private string _exeFile;
        public CommandLineExecutor(string exeFile)
        {
            _exeFile = exeFile;
        }


        public Dictionary<string, string> GetDefaultArguments()
        {
            var dics = new Dictionary<string, string>();
            dics.Add("ip", "192.168.1.109");
            dics.Add("port", "5005");
            dics.Add("machin-no", "1");

           

            return dics;
        }
        public T ExecuteCommand<T>(string command,Dictionary<string,string> arguments, ICommandLineParser<T> parser)
        {
            var p = "";

            foreach(var arg in arguments)
            {
                p += " --" + arg.Key + " " + arg.Value;
            }
            var output = RunCommand(command, p);

            if (output.Contains("NoData"))
                return default(T);

            var result = parser.Parse(output);

            return result;
        }

        private string RunCommand(string command, string arguments)
        {
            var process = new Process();

            process.StartInfo.FileName =_exeFile;
            if (!string.IsNullOrEmpty(arguments))
            {
                process.StartInfo.Arguments = command + " " + arguments;
            }

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            var stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) => stdOutput.AppendLine(args.Data); 
            // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

            string stdError = null;
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                stdError = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing " + Format(command, arguments) + ": " + e.Message, e);
            }

            if (process.ExitCode == 0)
            {
                return AnalyzeData(stdOutput.ToString());
            }
            else
            {
                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(stdError))
                {
                    message.AppendLine(stdError);
                }

                if (stdOutput.Length != 0)
                {
                    message.AppendLine("Std output:");
                    message.AppendLine(stdOutput.ToString());
                }

                throw new Exception(Format(command, arguments) + " finished with exit code = " + process.ExitCode + ": " + message);
            }
        }

        private string AnalyzeData(string commandOutput)
        {
            if(String.IsNullOrEmpty(commandOutput))
            {
                throw new Exception("No Output");
            }

            var err = "Error:";

            if (commandOutput.StartsWith(err))
            {
                var rest = commandOutput.Replace(err, "");
                throw new Exception(rest);
            }
            return commandOutput;
        }

        private string Format(string filename, string arguments)
        {
            return "'" + filename +
                   ((string.IsNullOrEmpty(arguments)) ? string.Empty : " " + arguments) +
                   "'";
        }
   

    }

}


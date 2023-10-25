using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Device Time")]
    class Gettime : BaseParameters
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var result = timmy.GetDeviceTime();

            if (result == null)
            {
                WriteErrorCode(timmy, console);
                timmy.DisConnect();
                return;
            }

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
            
            timmy.DisConnect();

        }
    }
}

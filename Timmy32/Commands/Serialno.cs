using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Serial No")]
    class Serialno : BaseParameters
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var result = timmy.GetSerialNo();

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }


}

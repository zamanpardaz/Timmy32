using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Product Code")]
    class Productcode : BaseParameters
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                console.WriteLine(Constants.FormatErrortMessage(Constants.ConnectionFailed));
                return;
            }

            var result = timmy.GetProductCode();

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
            timmy.DisConnect();

        }
    }


}

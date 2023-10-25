using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;

namespace Timmy32.Commands
{
    [Command(Description = "Power On")]
    class On
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();


            timmy.PowerOn();

            Console.WriteLine(Constants.FormatMessage(Constants.Done));
            
            timmy.DisConnect();

        }
    }


}

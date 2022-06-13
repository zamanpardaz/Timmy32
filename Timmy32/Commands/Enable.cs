using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{

    [Command(Description = "Enable Device")]
    class Enable : BaseParameters
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if(!isConnected)
            {
                return;
            }

            var result=timmy.EnableDevice();

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }


}

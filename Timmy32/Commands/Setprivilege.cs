using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set Privilege")]
    class Setprivilege : BaseParameters
    {

        [Option(ShortName = "u")]
        public int UserId { get; set; }


        [Option(ShortName = "r")]
        public int Privilege { get; set; }



        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }



            var result = timmy.ModifyPrivilege(UserId, Privilege);

            if (result == false)
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

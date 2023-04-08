using System;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
 
    
    [Command(Description = "Set Expiration")]
    class SetUserExpiration : BaseParameters
    {

        [Option(ShortName = "u")]
        public int UserId { get; set; }


        [Option(ShortName = "s")]
        public string Start { get; set; }

        [Option(ShortName = "e")]
        public string End { get; set; }

        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }


            var s = DateTime.Parse(Start);
            var e = DateTime.Parse(End);

            var result = timmy.SetValidExpireDate(UserId, s,e);

            if (result == false)
            {
                WriteErrorCode(timmy, console);
                return;
            }

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }
}
using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set User verification Mode")] 
    class VerificationMode:BaseParameters
    {
        [Option(ShortName = "u")]
        public long UserId { get; set; }


        [Option(ShortName = "m")]
        public int Mode { get; set; }

     

        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }



            var result = timmy.SetUserVerificationMode(UserId, Mode);

            if(result==false)
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
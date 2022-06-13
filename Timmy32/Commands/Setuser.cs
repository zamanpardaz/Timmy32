using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set User")]
    class Setuser : BaseParameters
    {

        [Option(ShortName = "u")]
        public int UserId { get; set; }


        [Option(ShortName = "n")]
        public string Name { get; set; }

     

        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }



            var result = timmy.SetUser(new User()
            {
                Id=UserId,
                Name=Name,
                Enabled=true,
                Privilege=0
            });

            if(result==false)
            {
                WriteErrorCode(timmy, console);
                return;
            }

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }
}

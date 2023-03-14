using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set User UTF*")]
    class SetuserUTF8 : BaseParameters
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



            var name = Encoding.UTF8.GetString(Convert.FromBase64String(Name));
            var result = timmy.SetUserUTF8(new User()
            {
                Id=UserId,
                Name=name,
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
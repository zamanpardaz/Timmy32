using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set a message to show user when the user set traffic")]

    class SetUserProfile:BaseParameters
    {
        [Option(ShortName = "u")]
        public int UserId { get; set; }


        [Option(ShortName = "s")]
        public string Message { get; set; }
        
        public void OnExecute(IConsole console)
        {
            var message = Encoding.UTF8.GetString(Convert.FromBase64String(Message));

            var timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }
            
            
            var result=timmy.SetUserProfile(UserId, message);

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
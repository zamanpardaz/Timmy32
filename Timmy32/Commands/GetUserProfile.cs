using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "get the message has been set by SetUserProfile command")]

    class GetUserProfile:BaseParameters
    {
        [Option(ShortName = "u")]
        public int UserId { get; set; }



        public void OnExecute(IConsole console)
        {
            var timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }
            
            
            var result=timmy.GetUserProfile(UserId);

            if(result==null)
            {
                WriteErrorCode(timmy, console);
                timmy.DisConnect();

                return;
            }
            
                
            var bytes = Encoding.UTF8.GetBytes(result);
            var base64 = Convert.ToBase64String(bytes);
            Console.WriteLine(Constants.FormatMessage(base64));
            
            timmy.DisConnect();
        }
    }
}
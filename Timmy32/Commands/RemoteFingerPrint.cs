using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Remote FingerPrint")]
    class RemoteFingerPrint:BaseParameters
    {
        
        [Option(ShortName = "u")]
        public long UserId { get; set; }
        
        [Option(ShortName = "f")]
        public int FingerIndex { get; set; }
        

        
        
        
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();


            var result = timmy.RemoteFingerPrint(UserId, FingerIndex);
            
            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }
}
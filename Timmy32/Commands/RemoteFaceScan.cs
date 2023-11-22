using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Remote Face Scan")]
    class RemoteFaceScan : BaseParameters
    {
        [Option(ShortName = "u")]
        public long UserId { get; set; }
        


        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();

            


            var result = timmy.RemoteFaceScan(UserId);
            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));

        }
    }
}
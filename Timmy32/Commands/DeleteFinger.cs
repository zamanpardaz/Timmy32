using System;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Delete Finger")]
    class DeleteFinger : BaseParameters
    {
        [Option(ShortName = "u",Description = "User ID")]
        public int UserId { get; set; }
        [Option(ShortName = "f",Description = "Finger Index")]
        public int FingerIndex { get; set; }
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var result = timmy.DeleteFinger(UserId,FingerIndex);

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }
}
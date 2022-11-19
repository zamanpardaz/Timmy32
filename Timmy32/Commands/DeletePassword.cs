using System;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Delete Password")]
    class DeletePassword : BaseParameters
    {
        [Option(ShortName = "u",Description = "User ID")]
        public int UserId { get; set; }
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var result = timmy.DeletePassword(UserId);

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }
}
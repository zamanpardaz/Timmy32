using System;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Delete User")]
    class DeleteUser : BaseParameters
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

            var result = timmy.DeleteUser(UserId);

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
            timmy.DisConnect();

        }
    }
}
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get User Name")]
    class Getname : BaseParameters
    {
        [Required]
        [Option]
        public int UserId { get; set; }
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var result = timmy.GetName(UserId);

            if (result == null)
                WriteErrorCode(timmy, console);

            var bytes = Encoding.UTF8.GetBytes(result);
            var base64 = Convert.ToBase64String(bytes);
            Console.WriteLine(Constants.FormatMessage(base64));
        }
    }
}

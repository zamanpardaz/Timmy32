using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Face")]
    class Getface : BaseParameters
    {

        [Option(ShortName = "u")]
        public int UserId { get; set; }

        [Option(Description ="Face Index is equal to 20-27 (Face 1 : 20-23 , Face 2 : 24-27)",ShortName ="f")]
        public int FaceIndex { get; set; }
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var bytes = timmy.GetFace(UserId, FaceIndex);

            if (bytes == null)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                return;
            }

            var result = Convert.ToBase64String(bytes);

            Console.WriteLine(Constants.FormatMessage(result));
        }
    }

}

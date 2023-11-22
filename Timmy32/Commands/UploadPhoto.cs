using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Upload Photo")]
    class UploadPhoto : BaseParameters
    {
        [Option(ShortName = "u",Description = "User ID")]
        public string UserId { get; set; }
        
        [Option(ShortName = "s",Description = "image address")]
        public string Source { get; set; }
        
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            timmy.DisableDevice();
            var photo = File.ReadAllBytes(Source);
            var result = timmy.UploadPhoto(MachinNo,long.Parse(UserId),photo );

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));

            timmy.EnableDevice();
            timmy.DisConnect();

        }
    }
}
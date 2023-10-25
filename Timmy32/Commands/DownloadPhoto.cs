using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Enable Device")]
    class DownloadPhoto : BaseParameters
    {


        [Required]
        [Option(Description = "User ID", ShortName = "u")]
        public string UserId { get; set; }

        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var photo = timmy.DownloadPhoto(MachinNo, int.Parse(UserId));

            if (photo == null)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                timmy.DisConnect();

                return;
            }

            Console.WriteLine(Constants.FormatMessage(photo));
            timmy.DisConnect();

        }
    }
}
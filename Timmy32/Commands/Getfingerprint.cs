using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Finger Print By Finger Index")]
    class Getfingerprint : BaseParameters
    {

        [Option(ShortName ="u")]
        public int UserId { get; set; }

        [Option(ShortName ="f")]
        public int FingerIndex { get; set; }
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy,console);

            if (!isConnected)
            {
                return;
            }

            var bytes = timmy.GetFingerPrint(UserId,FingerIndex);

            if(bytes== null)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                return;
            }

            var result = Convert.ToBase64String(bytes);

            Console.WriteLine(Constants.FormatMessage(result));
        }
    }


}

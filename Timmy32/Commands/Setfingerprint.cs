using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set Finger Print By Finger Index")]
    class Setfingerprint : BaseParameters
    {

        [Option(ShortName = "u")]
        public int UserId { get; set; }

        [Option(ShortName = "f")]
        public int FingerIndex { get; set; }


        [Option(Description ="finger print data in base 64 format",ShortName ="d")]
        public string Data { get; set; }
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }


            var bytes = Convert.FromBase64String(Data);
            var result = timmy.SetFingerPrint(UserId, FingerIndex,bytes);



            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
        }
    }


}

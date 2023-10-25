using System;
using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get All Finger Prints")]
    class Getfingerprints : BaseParameters
    {

        [Option(ShortName ="u")]
        public int UserId { get; set; }

        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy,console);

            if (!isConnected)
            {
                return;
            }

            var fingers = new List<string>();

            timmy.DisableDevice();
            for (int i = 0; i < 10; i++)
            {
                var bytes = timmy.GetFingerPrint(UserId,i);

                if(bytes== null)
                {
                    continue;
                }

                var result =i+":"+ Convert.ToBase64String(bytes);
                fingers.Add(result);
            }

            if(fingers.Count== 0)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                timmy.EnableDevice();
                timmy.DisConnect();

                return;
            }

            var finalResult = string.Join(",", fingers);
            Console.WriteLine(Constants.FormatMessage(finalResult));
            timmy.EnableDevice();

            timmy.DisConnect();

        }
    }
}
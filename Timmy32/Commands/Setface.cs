using McMaster.Extensions.CommandLineUtils;
using System;
using System.Text;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Face")]
    class Setface : BaseParameters
    {

        [Option(ShortName = "u")]
        public int UserId { get; set; }

        [Option(Description = "Face Index is equal to 20-27 (Face 1 : 20-23 , Face 2 : 24-27)",ShortName ="f")]
        public int FaceIndex { get; set; }

        [Option(Description = "Face data in base 64 format",ShortName ="d")]
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
            

            var result = timmy.SetFace(UserId, FaceIndex, bytes);

            Console.WriteLine(Constants.FormatMessage(result.ToString().ToLower()));
            
            timmy.DisConnect();

        }
    }

}

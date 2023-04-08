using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using Timmy32.Exceptions;

namespace Timmy32.Models
{
    class BaseParameters
    {
        public BaseParameters()
        {
            MachinNo = 1;
        }

        [Required]
        [Option(Description ="IP Of The Device")]
        public string Ip { get; set; }

        [Required]
        [Option(Description = "Port Of The Device",ShortName ="p")]
        public int Port { get; set; }

        [Option(Description = "Password Of The Device",ShortName ="w")]
        public int Password { get; set; }

        [Option(Description = "MachinNumber Of The Device. Default Value is 1")]
        public int MachinNo { get; set; }


        protected void WriteErrorCode(Timmy timmy,IConsole console)
        {
            console.WriteLine(Constants.FormatErrortMessage(timmy.GetError().ToString()));
        }

        protected bool Connect(Timmy timmy, IConsole console)
        {
            var isConnected = false;
          
             isConnected=  timmy.ConnectByIp(Ip, Port, Password, MachinNo);
            
            if(!isConnected)
                console.WriteLine(Constants.FormatErrortMessage(Constants.ConnectionFailed));

            return isConnected;
        }
    }
}

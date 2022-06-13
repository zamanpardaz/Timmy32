using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Set Device Time")]
    class Settime : BaseParameters
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = timmy.ConnectByIp(Ip, Port, Password, MachinNo);

            if (!isConnected)
            {
                console.WriteLine(Constants.FormatErrortMessage(Constants.ConnectionFailed));
                return;
            }

            timmy.SetDeviceTime();


            console.WriteLine(Constants.FormatMessage(Constants.Done));
        }
    }
}

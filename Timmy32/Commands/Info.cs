using McMaster.Extensions.CommandLineUtils;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Device Info")]
    class Info : BaseParameters
    {


        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var info = timmy.GetDeviceInfo();


            console.Write(info.GlogWarning + ",");
            console.Write(info.Language + ",");
            console.Write(info.LockOperate + ",");
            console.Write(info.MessageCount + ",");
            console.Write(info.PowerOffTime + ",");
            console.Write(info.ReVerifyTime + ",");
            console.Write(info.SlogWarning);



        }
    }

}

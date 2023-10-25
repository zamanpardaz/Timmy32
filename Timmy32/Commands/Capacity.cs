using McMaster.Extensions.CommandLineUtils;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Device Capacities")]
    class Capacity : BaseParameters
    {


        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var capacity = timmy.GetCapacity();

            
            console.Write(capacity.ManagerCount + ",");
            console.Write(capacity.UserCount + ",");
            console.Write(capacity.PasswordCount + ",");
            console.Write(capacity.GLogCount + ",");
            console.Write(capacity.SLogCount + ",");
            console.Write(capacity.FingerPrintCount + ",");
            console.Write(capacity.WhatCount);

            timmy.DisConnect();


        }
    }

}

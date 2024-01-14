using System;
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


            var output = String.Join(",", new[]
            {
                capacity.ManagerCount,capacity.UserCount,capacity.PasswordCount,capacity.GLogCount,
                capacity.SLogCount,capacity.FingerPrintCount,capacity.WhatCount,capacity.UserCapacity,
                capacity.FaceCapacity,capacity.FingerPrintCapacity,capacity.LogCapacity
            });
            console.Write(output);

            timmy.DisConnect();


        }
    }

}

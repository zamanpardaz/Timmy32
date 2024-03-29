﻿using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get New Traffic Logs")]
    class Newlogs : BaseParameters
    {


        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var logs = timmy.GetLogs(true);

            if (logs.Count == 0)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                timmy.DisConnect();

                return;
            }

            foreach (var log in logs)
            {
                console.Write(log.dwEnrollNumber + ",");
                console.Write(log.dwInout + ",");
                console.Write(log.dwVerifyMode + ",");
                console.Write(log.dwEvent + ",");
                console.Write(log.dwYear + ",");
                console.Write(log.dwMonth + ",");
                console.Write(log.dwDay + ",");
                console.Write(log.dwHour + ",");
                console.Write(log.dwMinute + ",");
                console.Write(log.dwSecond);
                console.WriteLine();
            }
            timmy.DisConnect();

        }
    }

}

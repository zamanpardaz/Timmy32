using System;
using Timmy32;

namespace Commander
{
    public class CapacityParser : ICommandLineParser<DeviceCapacity>
    {
        public DeviceCapacity Parse(string data)
        {
            var fields = data.Split(new[] { "," }, StringSplitOptions.None);

            var capacity = new DeviceCapacity()
            {
                ManagerCount = int.Parse(fields[0]),
                UserCount = int.Parse(fields[1]),
                PasswordCount = int.Parse(fields[2]),
                GLogCount = int.Parse(fields[3]),
                SLogCount = int.Parse(fields[4]),
                FingerPrintCount = int.Parse(fields[5]),
                WhatCount = int.Parse(fields[6])

            };

            return capacity;
        }
    }

}


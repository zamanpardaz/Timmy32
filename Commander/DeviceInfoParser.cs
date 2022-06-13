using System;
using Timmy32;

namespace Commander
{
    public class DeviceInfoParser : ICommandLineParser<DeviceInfo>
    {
        public DeviceInfo Parse(string data)
        {
            var fields = data.Split(new[] { "," }, StringSplitOptions.None);


            return new DeviceInfo()
            {
                GlogWarning=int.Parse(fields[0]),
                Language=int.Parse(fields[1]),
                LockOperate=int.Parse(fields[2]),
                MessageCount=int.Parse(fields[3]),
                PowerOffTime=int.Parse(fields[4]),
                ReVerifyTime=int.Parse(fields[5]),
                SlogWarning=int.Parse(fields[6])
            };
        }
    }

}


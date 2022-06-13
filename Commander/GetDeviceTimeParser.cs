using System;

namespace Commander
{
    public class GetDeviceTimeParser : ICommandLineParser<DateTime>
    {
        public DateTime Parse(string data)
        {
            var rawData = data.Replace("Result:", "");
            return DateTime.Parse(rawData);
        }
    }

}


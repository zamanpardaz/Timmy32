using System;
using System.Collections.Generic;
using Timmy32;

namespace Commander
{
    public class GetLogsParser : ICommandLineParser<List<GeneralLogInfo>>
    {
        public List<GeneralLogInfo> Parse(string data)
        {

            if (data == "Result:NoData")
                return null;

            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var list = new List<GeneralLogInfo>();

            foreach(var line in lines)
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                var fields = line.Split(new[] { "," }, StringSplitOptions.None);

                list.Add(new GeneralLogInfo()
                {
                    dwEnrollNumber = int.Parse(fields[0]),
                    dwInout = int.Parse(fields[1]),
                    dwVerifyMode = int.Parse(fields[2]),
                    dwEvent = int.Parse(fields[3]),
                    dwYear = int.Parse(fields[4]),
                    dwMonth = int.Parse(fields[5]),
                    dwDay = int.Parse(fields[6]),
                    dwHour = int.Parse(fields[7]),
                    dwMinute = int.Parse(fields[8]),
                    dwSecond = int.Parse(fields[9])
                });
            }

            return list;
            
        }
    }
}


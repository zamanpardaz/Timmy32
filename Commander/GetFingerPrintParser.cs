using System;

namespace Commander
{
    public class GetFingerPrintParser : ICommandLineParser<byte[]>
    {
        public byte[] Parse(string data)
        {
            var r = data.Replace("Result:", "");
            var bytes = Convert.FromBase64String(r);
            return bytes;
        }
    }

}


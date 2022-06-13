using System;
using System.Text;

namespace Commander
{
    public class PersianStringParser : ICommandLineParser<string>
    {
        public string Parse(string data)
        {
            var r = data.Replace("Result:", "").Replace(Environment.NewLine,"");
            
            var bytes = Convert.FromBase64String(r);
            var utf8 = Encoding.UTF8.GetString(bytes);

            return utf8;
        }
    }
}


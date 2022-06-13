using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timmy32;

namespace Commander
{
    public class UserParser : ICommandLineParser<List<User>>
    {
        public List<User> Parse(string data)
        {
            if (data == "Result=NoData")
                return null;

            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var list = new List<User>();
            foreach(var line in lines)
            {
                var fields=line.Split(new[] { "," }, StringSplitOptions.None);

                var base64 = fields[1];
                var bytes = Convert.FromBase64String(base64);
                var utf8 = Encoding.UTF8.GetString(bytes);
                list.Add(new User()
                {
                    Id = int.Parse(fields[0]),
                    Enabled = bool.Parse(fields[3]),
                    Privilege = int.Parse(fields[2]),
                    Name = utf8
                });
            }

            return list;
        }
    }
}

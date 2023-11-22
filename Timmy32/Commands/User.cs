using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get User By Id")]
    class GetUser : BaseParameters
    {
        [Option(ShortName = "u")]
        public long UserId { get; set; }

        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            var user = timmy.GetUser(UserId);

            if (user==null)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                timmy.DisConnect();

                return;
            }
            
            var name = user.Name;
            var base64 = "";

            if (String.IsNullOrEmpty(name) == false)
            {
                var bytes = Encoding.UTF8.GetBytes(name);
                base64 = Convert.ToBase64String(bytes);
            }

            console.Write(user.Id + ",");
            console.Write(base64 + ",");
            console.Write(user.Privilege + ",");
            console.Write(user.Enabled);
            console.WriteLine();
            

            timmy.DisConnect();

        }
    }
}
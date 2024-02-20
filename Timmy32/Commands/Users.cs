using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get Users")]
    class Users : BaseParameters
    {


        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                return;
            }

            List<User> users = new List<User>();

            if (timmy.IsAI())
            {
                users=timmy.GetAllUsers();

            }
            else
            {
                users = timmy.GetUsers();
            }


            if (users.Count == 0)
            {
                console.WriteLine(Constants.FormatMessage(Constants.NoData));
                timmy.DisConnect();

                return;
            }

            foreach (var user in users)
            {
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
                console.Write(user.Enabled+",");
                console.Write(user.Style);
                console.WriteLine();
            }

            timmy.DisConnect();

        }
    }
}

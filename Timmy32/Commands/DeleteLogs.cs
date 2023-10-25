using McMaster.Extensions.CommandLineUtils;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Delete Logs")]
    class DeleteLogs : BaseParameters
    {
        public void OnExecute(IConsole console)
        {
            Timmy timmy = new Timmy();
            var isConnected = Connect(timmy, console);

            if (!isConnected)
            {
                console.WriteLine(Constants.FormatErrortMessage(Constants.ConnectionFailed));
                return;
            }

            timmy.DeleteAllLogs();


            console.WriteLine(Constants.FormatMessage(Constants.Done));
            timmy.DisConnect();

        }
    }
}
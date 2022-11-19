
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Text;
using Timmy32.Commands;

//namespace Timmy32
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var timmy = new Timmy();
//            timmy.ConnectByIp("192.168.1.224", 5005, 0, 1);
//            var s=timmy.GetName(1);
//            var bytes = Encoding.UTF8.GetBytes(s);
//            var b = Convert.ToBase64String(bytes);

//            var mybytes = Convert.FromBase64String(b);
//            var utf = Encoding.UTF8.GetString(mybytes);
           
//        }


//    }
//}



namespace Timmy32
{
    [Command(Description = "Timmy 32 Bits DLL as CLI")]
    [Subcommand("enable", typeof(Enable))]
    [Subcommand("disable", typeof(Disable))]
    [Subcommand("capacity", typeof(Capacity))]
    [Subcommand("clear", typeof(Clear))]
    [Subcommand("getface", typeof(Getface))]
    [Subcommand("getfingerprint", typeof(Getfingerprint))]
    [Subcommand("getname", typeof(Getname))]
    [Subcommand("gettime", typeof(Gettime))]
    [Subcommand("info", typeof(Info))]
    [Subcommand("logs", typeof(Logs))]
    [Subcommand("newlogs", typeof(Newlogs))]
    [Subcommand("off", typeof(Off))]
    [Subcommand("on", typeof(On))]
    [Subcommand("productcode", typeof(Productcode))]
    [Subcommand("serialno", typeof(Serialno))]
    [Subcommand("setcardno", typeof(Setcardno))]
    [Subcommand("setface", typeof(Setface))]
    [Subcommand("setfingerprint", typeof(Setfingerprint))]
    [Subcommand("setpassword", typeof(Setpassword))]
    [Subcommand("settime", typeof(Settime))]
    [Subcommand("setuser", typeof(Setuser))]
    [Subcommand("users", typeof(Users))]
    [Subcommand("setprivilege", typeof(Setprivilege))]
    [Subcommand("version", typeof(Commands.Version))]
    [Subcommand("deluser", typeof(Commands.DeleteUser))]
    [Subcommand("delpass", typeof(Commands.DeletePassword))]
    [Subcommand("delfinger", typeof(Commands.DeleteFinger))]
    [Subcommand("dellogs", typeof(Commands.DeleteLogs))]
    class Program
    {
        static int Main(string[] args)
        {

            return CommandLineApplication.Execute<Program>(args);
        }

        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify a subcommand.");
            console.WriteLine();
            app.ShowHelp();
            return 1;
        }
    }
}

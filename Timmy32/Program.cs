
using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Text;
using Timmy32.Commands;

// namespace Timmy32
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var timmy = new Timmy();
//             timmy.ConnectByIp("192.168.1.225", 5005, 0, 1);
//             var bret = timmy.SetCardNo(1, 0,1001);
//             var err = timmy.GetError();
//
//         }
//
//
//     }
// }



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
    [Subcommand("setuserutf8", typeof(SetuserUTF8))]
    [Subcommand("users", typeof(Users))]
    [Subcommand("setprivilege", typeof(Setprivilege))]
    [Subcommand("version", typeof(Commands.Version))]
    [Subcommand("deluser", typeof(Commands.DeleteUser))]
    [Subcommand("delpass", typeof(Commands.DeletePassword))]
    [Subcommand("delfinger", typeof(Commands.DeleteFinger))]
    [Subcommand("dellogs", typeof(Commands.DeleteLogs))]
    [Subcommand("setExpire",typeof(Commands.SetUserExpiration))]
    [Subcommand("downloadphoto",typeof(Commands.DownloadPhoto))]
    [Subcommand("uploadphoto",typeof(Commands.UploadPhoto))]
    class Program
    {
        static int Main(string[] args)
        {
            // Timmy timmy = new Timmy();
            // timmy.ConnectByIp("192.168.1.225", 5005);
            // var users = timmy.SetUser(new User()
            // {
            //     Id = 1002,
            //     Name = "علی یگانه مقدم"÷ثبت 
            // });
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


using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Timmy32.Commands;
//
// namespace Timmy32
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var timmy = new Timmy();
//             var isConnected=timmy.ConnectByIp("192.168.20.53",5005,0,1);
//             if (isConnected == false)
//             {
//                 Console.WriteLine("Timmy Not Connected");
//                 return;
//             }
//
//             timmy.DisableDevice();
//
//             var enroll = 0;
//             for (int i = 0; i < 100; i++)
//             {
//                 enroll++;
//                 var u = "User " + enroll;
//             
//                var result= timmy.SetUser(new User()
//                 {
//                     Enabled = true,
//                     Name = u,
//                     Id = enroll
//                 });
//                 
//                 
//             }
//
//             
//
//             
//             // var start = DateTime.Now;
//             //
//             // // var users = timmy.GetAllUsers();
//             // // var error = timmy.GetError();
//             //
//             //
//             //
//             // var end = DateTime.Now;
//             //
//             // var span = end - start;
//             // // Console.WriteLine("Users:" + users.Count);
//             // // Console.WriteLine("Error Code:" + error);
//             // Console.WriteLine("Seconds:" + span.TotalSeconds + " - Minutes: " + span.TotalMinutes);
//
//             
//             timmy.EnableDevice();
//             timmy.DisConnect();
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
    [Subcommand("getfingerprints",typeof(Commands.Getfingerprints))]
    [Subcommand("User",typeof(Commands.GetUser))]
    [Subcommand("RemoteFaceScan",typeof(Commands.RemoteFaceScan))]
    [Subcommand("RemoteFingerPrint",typeof(Commands.RemoteFingerPrint))]
    [Subcommand("VerificationMode",typeof(Commands.VerificationMode))]
    [Subcommand("getuserprofile",typeof(Commands.GetUserProfile))]
    [Subcommand("setuserprofile",typeof(Commands.SetUserProfile))]
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

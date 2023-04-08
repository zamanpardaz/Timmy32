namespace Timmy32.Exceptions
{
    class Constants
    {

        public static string ConnectionFailed = "ConnectionFailed";
        public static string NoData = "NoData";
        public static string Done = "Done";



        public static string FormatMessage(string message)
        {
            return "Result:" + message;
        }

        public static string FormatErrortMessage(string message)
        {
            return "Error:" + message;
        }

    
    }
}

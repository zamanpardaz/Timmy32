namespace Commander
{
    public class DoneParser : ICommandLineParser<bool>
    {
        public bool Parse(string data)
        {
            var r = data.Replace("Result:", "");
            return r == "Done";
        }
    }

}


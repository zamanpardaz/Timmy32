namespace Commander
{

    public class StringParser : ICommandLineParser<string>
    {
        public string Parse(string data)
        {
            var r = data.Replace("Result:", "");


            return r;
        }
    }
}


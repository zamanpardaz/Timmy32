namespace Commander
{
    public class BoolParser : ICommandLineParser<bool>
    {
        public bool Parse(string data)
        {
            var r = data.Replace("Result:", "");

            return bool.Parse(r);
        }
    }

}


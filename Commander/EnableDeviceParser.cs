namespace Commander
{
    public class EnableDeviceParser : ICommandLineParser<bool>
    {
        public bool Parse(string data)
        {
            var r = data.Replace("Result:", "");

            return bool.Parse(r);
        }
    }
}


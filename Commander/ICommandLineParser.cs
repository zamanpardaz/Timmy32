namespace Commander
{
    public interface ICommandLineParser<T>
    {
        T Parse(string data);
    }

}


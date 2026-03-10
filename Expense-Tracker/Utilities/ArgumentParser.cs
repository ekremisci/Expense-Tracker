namespace ExpenseTracker.Utilities;

public static class ArgumentParser
{
    public static Dictionary<string, string> Parse(string[] args)
    {
        var dict = new Dictionary<string, string>();
        for (int i = 1; i < args.Length; i += 2)
        {
            if (i + 1 < args.Length) dict[args[i]] = args[i + 1];
        }
        return dict;
    }
}
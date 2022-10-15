namespace calc;

internal class Logger
{
    public static readonly Logger Instance = new(Console.Error);
    public TextWriter Output { get; init; }

    public Logger(TextWriter output)
    {
        Output = output;
    }

    public void Log(string message) => Output.WriteLine(message);
    public void Log(string message, Severity severity, FilePos pos = default)
    {
        string sev = severity switch
        {
            Severity.Information => "\x1b[96minfo:\x1b[0m ",
            Severity.Warning =>  "\x1b[95mwarning:\x1b[0m ",
            Severity.Error => "\x1b[91merror:\x1b[0m ",
            _ => "\x1b[93mmessage:\x1b[0m ",
        };

        Output.WriteLine($"{pos}\t{sev,18}{message}");
    }
}

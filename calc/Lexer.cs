namespace calc;

internal class Lexer
{
    TextReader _input;
    int _cur;
    int Cur => _cur;
    public double NuberToken { get; private set; }
    public string StringToken { get; private set; }


    public Lexer(TextReader input)
    {
        _input = input;
    }



    private int NextChar() => _cur = _input.Read();
}

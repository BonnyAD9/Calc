using System.Text;

namespace calc;

internal class Lexer
{
    readonly TextReader _input;
    char _cur = ' ';
    char Cur => _cur;
    bool _eof = false;
    bool Eof => _eof;
    const string punctuation = "()[]{},;_^";
    readonly StringBuilder _string = new();
    int col = 0;
    int line = 1;
    public double NumberToken { get; private set; }
    public string StringToken { get; private set; } = "";
    public FilePos TokenPosition { get; private set; }

    public Lexer(TextReader input)
    {
        _input = input;
    }

    public Token Next()
    {
        while (!Eof && char.IsWhiteSpace(Cur))
            NextChar();

        if (Eof)
            return Token.Eof;

        TokenPosition = new(line, col);

        if (char.IsDigit(Cur))
            return ReadNumber();

        if (char.IsLetter(Cur))
            return ReadIdentifier();

        if (!punctuation.Contains(Cur))
            return ReadOperator();

        StringToken = Cur.ToString();
        return (Token)CharNext();
    }

    Token ReadNumber()
    {
        _string.Clear();
        bool dot = false;

        do
        {
            _string.Append(Cur);
            NextChar();
        } while (!Eof && (char.IsDigit(Cur) || (!dot && (dot = Cur == '.'))));

        NumberToken = double.Parse(_string.ToString());
        return Token.Number;
    }

    Token ReadIdentifier()
    {
        _string.Clear();

        do
        {
            _string.Append(Cur);
            NextChar();
        } while (!Eof && char.IsLetter(Cur));

        StringToken = _string.ToString();
        return Token.Operator;
    }

    Token ReadOperator()
    {
        _string.Clear();

        do
        {
            _string.Append(Cur);
            NextChar();
        } while (!Eof && !punctuation.Contains(Cur) && !char.IsLetterOrDigit(Cur) && !char.IsWhiteSpace(Cur));

        StringToken = _string.ToString();
        return Token.Operator;
    }

    private char NextChar()
    {
        int c = _input.Read();
        _eof = c == -1;
        col++;
        if (c == '\n')
        {
            col = 0;
            line++;
        }
        return _cur = (char)c;
    }

    private char CharNext()
    {
        char c = _cur;
        NextChar();
        return c;
    }
}

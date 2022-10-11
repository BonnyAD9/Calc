namespace calc;

internal enum Token
{
    Eof = -1,
    Number,
    Operator,
    OpenBracket = '(',
    CloseBracket = ')',
    OpenAbs = '[',
    CloseAbs = ']',
    OpenSet = '{',
    CloseSet = '}',
    Comma = ',',
    Semicolon = ';',
    Lower = '_',
    Upper = '^',
}

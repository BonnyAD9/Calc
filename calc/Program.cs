using calc;

args = new[] { "5+5*sin(pi)" };

if (args.Length != 1)
{
    Console.WriteLine("Invalid number of arguments");
    return;
}


TextReader tr = new StringReader(args[0]);

Lexer lex = new(tr);

SymbolTable sym = new();

sym.AddConstants(
    ("pi", Math.PI),
    ("e",  Math.E)
);

sym.AddUnary(
    new("-", a => -a),
    new("abs",  a => Math.Abs(a)),
    new("sin",  a => Math.Sin(a)),
    new("cos",  a => Math.Cos(a)),
    new("tan",  a => Math.Tan(a)),
    new("acos", a => Math.Acos(a)),
    new("asin", a => Math.Asin(a)),
    new("sqrt", a => Math.Sqrt(a))
);

sym.AddBinary(
    new("+",  10, (a, b) => a + b),
    new("-",  10, (a, b) => a - b),
    new("*",  20, (a, b) => a * b),
    new("*-", 20, (a, b) => a * -b),
    new("/",  20, (a, b) => a / b),
    new("rt", 40, (a, b) => Math.Pow(b, 1 / a)),
    new("^",  40, (a, b) => Math.Pow(a, b))
);

Parser par = new(lex, sym);

Console.WriteLine(par.Parse().GetValue());

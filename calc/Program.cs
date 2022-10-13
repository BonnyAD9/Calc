using calc;

// args = new[] { "2*-3^2" };

if (args.Length != 1)
{
    Console.WriteLine("Invalid number of arguments");
    return;
}


TextReader tr = new StringReader(args[0]);

Lexer lex = new(tr);

SymbolTable sym = new() { };

sym.AddConstants(
    ("pi", Math.PI),
    ("e",  Math.E)
);

sym.AddUnary(
    new("-",    20, a => -a),
    new("abs",  30, a => Math.Abs(a)),
    new("sin",  30, a => Math.Sin(a)),
    new("cos",  30, a => Math.Cos(a)),
    new("tan",  30, a => Math.Tan(a)),
    new("acos", 30, a => Math.Acos(a)),
    new("asin", 30, a => Math.Asin(a)),
    new("sqrt", 30, a => Math.Sqrt(a))
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

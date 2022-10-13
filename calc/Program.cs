using calc;
using calc.Operators;

// args = new[] { "sin^-1(1)*2" };

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
    new UnaryOperator("-",    20, a => -a),
    new UnaryOperator("abs",  30, a => Math.Abs(a)),
    new UnaryOperator("sqrt", 30, a => Math.Sqrt(a)),

    new UnaryOperatorU("sin", 30, (a, u) => u switch
    {
        null => Math.Sin(a),
        -1   => Math.Asin(a),
        _    => Math.Pow(Math.Sin(a), u.Value),
    }),

    new UnaryOperatorU("cos", 30, (a, u) => u switch
    {
        null => Math.Cos(a),
        -1   => Math.Acos(a),
        _    => Math.Pow(Math.Cos(a), u.Value),
    }),

    new UnaryOperatorU("tan", 30, (a, u) => u switch
    {
        null => Math.Tan(a),
        -1   => Math.Atan(a),
        _    => Math.Pow(Math.Tan(a), u.Value),
    }),

    new UnaryOperatorU("asin", 30, (a, u) => u switch
    {
        null => Math.Asin(a),
        -1   => Math.Sin(a),
        _    => Math.Pow(Math.Asin(a), u.Value),
    }),

    new UnaryOperatorU("acos", 30, (a, u) => u switch
    {
        null => Math.Acos(a),
        -1   => Math.Cos(a),
        _    => Math.Pow(Math.Acos(a), u.Value),
    }),

    new UnaryOperatorU("atan", 30, (a, u) => u switch
    {
        null => Math.Atan(a),
        -1   => Math.Tan(a),
        _    => Math.Pow(Math.Atan(a), u.Value),
    })
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

using calc;
using calc.AST;
using calc.Operators;

// args = new[] { "sin^2(1)+cos^2(1)" };

if (args.Length != 1)
{
    Logger.Instance.Log("Invalid number of arguments", Severity.Error);
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
    // No index unary operators
    new UnaryOperator("-",    20, a => -a),
    new UnaryOperator("abs",  30, Math.Abs),
    new UnaryOperator("sqrt", 30, Math.Sqrt),

    // Lower and upper index unary operators
    new UnaryOperatorLU("log", 30, UnaryOperatorLU.MakeFun.UPower((a, l) => l.HasValue ? Math.Log(a, l.Value) : Math.Log10(a))),

    // upper index unary operators
    new UnaryOperatorU("ln",   30, UnaryOperatorU.MakeFun.Power(Math.Log)),
    new UnaryOperatorU("lb",   30, UnaryOperatorU.MakeFun.Power(Math.Log2)),
    new UnaryOperatorU("sin",  30, UnaryOperatorU.MakeFun.Goniometric(Math.Sin,  Math.Asin)),
    new UnaryOperatorU("cos",  30, UnaryOperatorU.MakeFun.Goniometric(Math.Cos,  Math.Acos)),
    new UnaryOperatorU("tan",  30, UnaryOperatorU.MakeFun.Goniometric(Math.Tan,  Math.Atan)),
    new UnaryOperatorU("asin", 30, UnaryOperatorU.MakeFun.Goniometric(Math.Asin, Math.Sin)),
    new UnaryOperatorU("acos", 30, UnaryOperatorU.MakeFun.Goniometric(Math.Acos, Math.Cos)),
    new UnaryOperatorU("atan", 30, UnaryOperatorU.MakeFun.Goniometric(Math.Atan, Math.Tan))
);

sym.AddBinary(
    new("+",  10, (a, b) => a + b),
    new("-",  10, (a, b) => a - b),
    new("*",  20, (a, b) => a * b),
    new("*-", 20, (a, b) => a * -b),
    new("/",  20, (a, b) => a / b),
    new("rt", 40, (a, b) => Math.Pow(b, 1 / a)),
    new("^",  40, Math.Pow)
);

Parser par = new(lex, sym);

Console.WriteLine(par.Parse());

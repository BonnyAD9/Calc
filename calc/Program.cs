using calc;
using calc.Operators;

//args = new[] { "x=5;x! +x*x" };
// args = new[] { "(10 3)" };

if (args.Length != 1)
{
    Logger.Instance.Log("Invalid number of arguments", Severity.Error);
    return;
}


TextReader tr = new StringReader(args[0]);

Lexer lex = new(tr);

SymbolTable sym = new() { };

sym.AddConstants(
    ("pi", Functions.Numeric(Math.PI)),
    ("e",  Functions.Numeric(Math.E))
);

sym.AddUnary(
    // No index unary operators
    new UnaryOperator("-",    20, Functions.Numeric(a => -a)),
    new UnaryOperator("abs",  30, Functions.Numeric(Math.Abs)),
    new UnaryOperator("sqrt", 30, Functions.Numeric(Math.Sqrt)),

    // Lower and upper index unary operators
    new UnaryOperatorLU("log", 30, Functions.Log),

    // upper index unary operators
    new UnaryOperatorU("ln",   30, Functions.Power(Functions.Numeric((Func<double, double>)Math.Log))),
    new UnaryOperatorU("lb",   30, Functions.Power(Functions.Numeric(Math.Log2))),
    new UnaryOperatorU("sin",  30, Functions.Goniometric(Math.Sin,  Math.Asin)),
    new UnaryOperatorU("cos",  30, Functions.Goniometric(Math.Cos,  Math.Acos)),
    new UnaryOperatorU("tan",  30, Functions.Goniometric(Math.Tan,  Math.Atan)),
    new UnaryOperatorU("asin", 30, Functions.Goniometric(Math.Asin, Math.Sin)),
    new UnaryOperatorU("acos", 30, Functions.Goniometric(Math.Acos, Math.Cos)),
    new UnaryOperatorU("atan", 30, Functions.Goniometric(Math.Atan, Math.Tan))
);

sym.AddBinary(
    new("=",   1, Functions.Equals),
    new("+",  10, Functions.Numeric((a, b) => a + b)),
    new("-",  10, Functions.Numeric((a, b) => a - b)),
    new("*",  20, Functions.Numeric((a, b) => a * b)),
    new("*-", 20, Functions.Numeric((a, b) => a * -b)),
    new("/",  20, Functions.Numeric((a, b) => a / b)),
    new("C",  30, Functions.Combination),
    new("P",  30, Functions.Variation),
    new("rt", 40, Functions.Numeric((a, b) => Math.Pow(b, 1 / a))),
    new("^",  40, Functions.Numeric(Math.Pow))
);

sym.AddPostfix(
    new PostfixOperator("!", 30, Functions.Factorial)
);

Parser par = new(lex, sym);

Console.WriteLine(par.Parse());

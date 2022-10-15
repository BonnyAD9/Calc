﻿using calc;
using calc.AST;
using calc.Operators;

// args = new[] { "log(10)" };

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
    new UnaryOperator("abs",  30, a => Math.Abs(a)),
    new UnaryOperator("sqrt", 30, a => Math.Sqrt(a)),

    // Lower and upper index unary operators
    new UnaryOperatorLU("log", 30, UnaryOperatorLU.MakeFun.UPower((a, l) => l.HasValue ? Math.Log(a, l.Value) : Math.Log10(a))),

    // upper index unary operators
    new UnaryOperatorU("ln", 30, (a, u) => u.HasValue ? Math.Pow(Math.Log(a), u.Value) : Math.Log(a)),
    new UnaryOperatorU("lb", 30, (a, u) => u.HasValue ? Math.Pow(Math.Log2(a), u.Value) : Math.Log(a)),

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

var res = par.Parse();
if (res == Expr.Null)
    return;
Console.WriteLine(res.GetValue());

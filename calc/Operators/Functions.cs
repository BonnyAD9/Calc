using calc.AST;

namespace calc.Operators;

internal static class Functions
{
    public static IExpression Numeric(double constant) => Expr.Constant(constant);

    public static Func<IExpression, IExpression> Numeric(Func<double, double> unary) =>
        a => a is ConstantExpression ca
            ? Expr.Constant(unary(ca.Value))
            : Expr.Error(a, "This unary operator accepts only numbers.");

    public static Func<IExpression, IExpression, IExpression> Numeric(Func<double, double, double> binary) =>
        (l, r) => l is ConstantExpression cl && r is ConstantExpression cr
            ? Expr.Constant(binary(cl.Value, cr.Value))
            : Expr.Error(l is ConstantExpression ? r : l, "This binary operator accepts only numbers.");

    public static Func<IExpression, IExpression, IExpression> Power(Func<IExpression, IExpression> unary) => (a, u) =>
    {
        if (u == Expr.Null)
            return unary(a);

        if (u is not ConstantExpression cu)
            return Expr.Error(u, "This operator's upper index must be a number.");

        var e = unary(a);
        if (e is not ConstantExpression ce)
            return Expr.Error(e, "Only numbers can be raised");

        return Expr.Constant(Math.Pow(ce.Value, cu.Value));
    };

    public static Func<IExpression, IExpression, IExpression, IExpression> Power(Func<IExpression, IExpression, IExpression> unaryl) => (a, l, u) =>
    {
        if (u == Expr.Null)
            return unaryl(a, l);

        if (u is not ConstantExpression cu)
            return Expr.Error(u, "This operator's upper index must be a number.");

        var e = unaryl(a, l);
        if (e is not ConstantExpression ce)
            return Expr.Error(e, "Only numbers can be raised");

        return Expr.Constant(Math.Pow(ce.Value, cu.Value));
    };

    public static Func<IExpression, IExpression, IExpression> Goniometric(Func<double, double> fun, Func<double, double> afun) => (a, u) =>
    {
        if (a is not ConstantExpression ca)
            return Expr.Error(a, "Goniometric functions work only on numbers.");

        if (u == Expr.Null)
            return Expr.Constant(fun(ca.Value));

        if (u is not ConstantExpression cu)
            return Expr.Error(u, "Goniometric functions must have a number as a upper index.");

        return Expr.Constant(cu.Value == -1
            ? afun(ca.Value)
            : Math.Pow(fun(ca.Value), cu.Value)
        );
    };

    public static IExpression Log(IExpression a, IExpression l, IExpression u) => (a, l, u) switch
    {
        (ConstantExpression ca, NullExpressoin, NullExpressoin) => Expr.Constant(Math.Log10(ca.Value)),
        (ConstantExpression ca, ConstantExpression cl, NullExpressoin) => Expr.Constant(Math.Log(ca.Value, cl.Value)),
        (ConstantExpression ca, NullExpressoin, ConstantExpression cu) => Expr.Constant(Math.Pow(Math.Log10(ca.Value), cu.Value)),
        (ConstantExpression ca, ConstantExpression cl, ConstantExpression cu) => Expr.Constant(Math.Pow(Math.Log(ca.Value, cl.Value), cu.Value)),
        (ConstantExpression, NullExpressoin or ConstantExpression, _) => Expr.Error(u, "Upper index of logarithm must be a number"),
        (ConstantExpression, _, _) => Expr.Error(l, "Lower index of logarithm must be a number"),
        _ => Expr.Error(a, "Argument of logarithm must be a number")
    };

    public static IExpression Equals(IExpression l, IExpression r) => (l, r) switch
    {
        (ConstantExpression cl, ConstantExpression cr) => Expr.Constant(cl.Value == cr.Value ? 1 : 0),
        _ => Expr.Error(l, "Cannot compare the values"),
    };

    private static double Factorial(double n, double a = 1)
        => n <= 1 ? a : Factorial(n - 1, a * n);

    public static IExpression Factorial(IExpression a)
    {
        if (a is not ConstantExpression ca)
            return Expr.Error(a, "Factorial accepts only numbers");

        double val = Math.Truncate(Math.Abs(ca.Value));

        return Expr.Constant(Factorial(val));
    }

    private static double Variation(double n, double c, double a = 1)
        => n <= 1 || c <= 0 ? a : Variation(n - 1, c - 1, a * n); 

    public static IExpression Combination(IExpression l, IExpression r)
    {
        if (l is not ConstantExpression cl || r is not ConstantExpression cr)
            return Expr.Error(l is ConstantExpression ? r : l, "Combination accepts only numbers");

        double max = Math.Truncate(Math.Abs(cr.Value));
        double min = Math.Truncate(Math.Abs(cl.Value - max));
        
        if (min > max)
            (max, min) = (min, max);

        return Expr.Constant(Variation(Math.Truncate(Math.Abs(cl.Value)), min) / Factorial(min));
    }

    public static IExpression Variation(IExpression l, IExpression r)
    {
        if (l is not ConstantExpression cl || r is not ConstantExpression cr)
            return Expr.Error(l is ConstantExpression ? r : l, "Variation accepts only numbers");
        
        return Expr.Constant(Variation(Math.Truncate(Math.Abs(cl.Value)), Math.Truncate(Math.Abs(cr.Value))));
    }

    private static IExpression Sum(IEnumerable<double> insert, string name, IExpression a)
    {
        double sum = 0;
        Context c = new();
        foreach (var i in insert)
        {
            c.SetVariable(name, Expr.Constant(i));
            var e = a.GetValue(c);
            if (e is not ConstantExpression ce)
                return Expr.Error(e, "Can sum only numbers");
            sum += ce.Value;
        }

        return Expr.Constant(sum);
    }

    public static IEnumerable<double> Range(double start, double end)
    {
        for (; start <= end; start++)
            yield return start;
        yield break;
    }

    const int sumLim = 1000;

    public static IExpression Sum(IExpression a, IExpression l, IExpression u) => (l, u) switch
    {
        (NullExpressoin, NullExpressoin) => Sum(Range(1, sumLim), "i", a),
        (NullExpressoin, ConstantExpression ce) => Sum(Range(1, ce.Value), "i", a),
        (ConstantExpression ce, NullExpressoin) => Sum(Range(ce.Value, ce.Value + sumLim), "i", a),
        (VariableExpression va, NullExpressoin) => Sum(Range(1, sumLim), va.Name, a),
        (VariableExpression va, ConstantExpression ca) => Sum(Range(1, ca.Value), va.Name, a),
        (BinaryExpression
        {
            Operator: { Name: "=" },
            Left: VariableExpression name,
            Right: ConstantExpression start 
        }, ConstantExpression end) => Sum(Range(start.Value, end.Value), name.Name, a),
        (BinaryExpression
        {
            Operator: { Name: "=" },
            Left: VariableExpression name,
            Right: ConstantExpression start,
        }, NullExpressoin) => Sum(Range(start.Value, start.Value + sumLim), name.Name, a),
        _ => Expr.Error(l, "Invalid expression in upper or lower index of sum"),
    };
}

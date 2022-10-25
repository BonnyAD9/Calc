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
        (_, _) => Expr.Constant(0),
    };
}

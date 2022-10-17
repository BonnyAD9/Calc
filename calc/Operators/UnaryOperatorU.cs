namespace calc.Operators;

internal class UnaryOperatorU : IUnaryOperatorU
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly EvalFun evaluate;

    public delegate double EvalFun(double a, double? u = null);

    public UnaryOperatorU(string name, int precedence, EvalFun evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public double Evaluate(double a) => evaluate(a);
    public double Evaluate(double a, double u) => evaluate(a, u);

    public static class MakeFun
    {
        public static EvalFun Goniometric(Func<double, double> f, Func<double, double> af) => (a, u) => u switch
        {
            null => f(a),
            -1 => af(a),
            _ => Math.Pow(f(a), u.Value),
        };

        public static EvalFun Power(Func<double, double> f) => (a, u) => u.HasValue ? Math.Pow(f(a), u.Value) : f(a);
    }

    public override string ToString() => Name;
}

namespace calc.Operators;

internal class UnaryOperatorLU : IUnaryOperatorLU
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly EvalFun evaluate;

    public delegate double EvalFun(double a, double? l = null, double? u = null);

    public UnaryOperatorLU(string name, int precedence, EvalFun evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public double Evaluate(double a) => evaluate(a);
    double IUnaryOperatorL.Evaluate(double a, double l) => evaluate(a, l: l);
    double IUnaryOperatorU.Evaluate(double a, double u) => evaluate(a, u: u);
    public double Evaluate(double a, double l, double u) => evaluate(a, l, u);

    public static class MakeFun
    {
        public static EvalFun UPower(UnaryOperatorL.EvalFun f) => (a, l, u) => u.HasValue ? Math.Pow(f(a, l), u.Value) : f(a, l);
    }
}

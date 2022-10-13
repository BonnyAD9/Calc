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
}

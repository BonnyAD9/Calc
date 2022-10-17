namespace calc.Operators;

internal class UnaryOperatorL : IUnaryOperatorL
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly EvalFun evaluate;

    public delegate double EvalFun(double a, double? l = null);

    public UnaryOperatorL(string name, int precedence, EvalFun evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public double Evaluate(double a) => evaluate(a);
    public double Evaluate(double a, double l) => evaluate(a, l);

    public override string ToString() => Name;
}

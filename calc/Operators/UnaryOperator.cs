namespace calc.Operators;

internal class UnaryOperator : IUnaryOperator
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly Func<double, double> evaluate;

    public UnaryOperator(string name, int precedence, Func<double, double> evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public double Evaluate(double a) => evaluate(a);
    public override bool Equals(object? obj) => obj is UnaryOperator other && other.Precedence == Precedence && other.Name == Name;
    public override int GetHashCode() => Name.GetHashCode() ^ Precedence.GetHashCode();

    public override string ToString() => Name;
}

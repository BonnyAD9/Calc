namespace calc.Operators;

internal class UnaryOperator
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    public Func<double, double> Evaluate { get; init; }

    public UnaryOperator(string name, int precedence, Func<double, double> evaluate)
    {
        Name = name;
        Precedence = precedence;
        Evaluate = evaluate;
    }

    public override bool Equals(object? obj) => obj is UnaryOperator other && other.Precedence == Precedence && other.Name == Name;
    public override int GetHashCode() => Name.GetHashCode() ^ Precedence.GetHashCode();
}

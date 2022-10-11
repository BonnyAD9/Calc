namespace calc;

internal class UnaryOperator
{
    public string Name { get; init; }
    public Func<double, double> Evaluate { get; init; }

    public UnaryOperator(string name, Func<double, double> evaluate)
    {
        Name = name;
        Evaluate = evaluate;
    }

    public override bool Equals(object? obj) => obj is UnaryOperator other && other.Name == Name;
    public override int GetHashCode() => Name.GetHashCode();
}

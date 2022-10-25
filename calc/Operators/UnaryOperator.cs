using calc.AST;

namespace calc.Operators;

internal class UnaryOperator : IUnaryOperator
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly Func<IExpression, IExpression> evaluate;

    public UnaryOperator(string name, int precedence, Func<IExpression, IExpression> evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public IExpression Evaluate(IExpression a) => evaluate(a);
    public override bool Equals(object? obj) => obj is UnaryOperator other && other.Precedence == Precedence && other.Name == Name;
    public override int GetHashCode() => Name.GetHashCode() ^ Precedence.GetHashCode();

    public override string ToString() => Name;
}

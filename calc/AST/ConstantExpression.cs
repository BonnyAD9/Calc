namespace calc.AST;

internal class ConstantExpression : IExpression
{
    public double Value { get; init; }

    public ConstantExpression(double value)
    {
        Value = value;
    }

    public IExpression GetValue(IDictionary<string, IExpression> vals) => this;

    public override string ToString() => Value.ToString();
}

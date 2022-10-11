namespace calc.AST;

internal class ConstantExpression : IExpression
{
    public double Value { get; init; }

    public ConstantExpression(double value)
    {
        Value = value;
    }

    public double GetValue() => Value;
}

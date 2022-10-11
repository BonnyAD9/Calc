namespace calc.AST;

internal class UnaryExpression : IExpression
{
    public IExpression Arg { get; init; }
    public UnaryOperator Operator { get; init; }

    public UnaryExpression(IExpression arg, UnaryOperator @operator)
    {
        Arg = arg;
        Operator = @operator;
    }

    public double GetValue() => Operator.Evaluate(Arg.GetValue());
}

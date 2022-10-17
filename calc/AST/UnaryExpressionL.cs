using calc.Operators;

namespace calc.AST;

internal class UnaryExpressionL : IExpression
{
    public IExpression Arg { get; init; }
    public IExpression Lower { get; init; }
    public IUnaryOperatorL Operator { get; init; }

    public UnaryExpressionL(IExpression arg, IExpression lower, IUnaryOperatorL @operator)
    {
        Arg = arg;
        Lower = lower;
        Operator = @operator;
    }

    public IExpression GetValue(IDictionary<string, IExpression> vals) =>
        Expr.UnaryL(Operator, Arg.GetValue(vals), Lower.GetValue(vals));

    public override string ToString() => $"{Operator}_({Lower})({Arg})";
}

using calc.Operators;

namespace calc.AST;

internal class UnaryExpression : IExpression
{
    public IExpression Arg { get; init; }
    public IUnaryOperator Operator { get; init; }

    public UnaryExpression(IExpression arg, IUnaryOperator @operator)
    {
        Arg = arg;
        Operator = @operator;
    }

    public IExpression GetValue(IDictionary<string, IExpression> vals) =>
        Expr.Unary(Operator, Arg.GetValue(vals));

    public override string ToString() => $"{Operator}({Arg})";
}

using calc.Operators;

namespace calc.AST;

internal class UnaryExpressionU : IExpression
{
    public IExpression Arg { get; init; }
    public IExpression Upper { get; init; }
    public IUnaryOperatorU Operator { get; init; }

    public UnaryExpressionU(IExpression arg, IExpression upper, IUnaryOperatorU @operator)
    {
        Arg = arg;
        Upper = upper;
        Operator = @operator;
    }

    public IExpression GetValue(IDictionary<string, IExpression> vals) =>
        Expr.UnaryU(Operator, Arg.GetValue(vals), Upper.GetValue(vals));

    public override string ToString() => $"{Operator}^({Upper})({Arg})";
}

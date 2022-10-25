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

    public IExpression GetValue(Context context) =>
        Expr.UnaryU(Operator, Arg.GetValue(context), Upper.GetValue(context));

    public override string ToString() => $"{Operator}^({Upper})({Arg})";
}

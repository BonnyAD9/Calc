using calc.Operators;

namespace calc.AST;

internal class UnaryExpressionLU : IExpression
{
    public IExpression Arg { get; init; }
    public IExpression Lower { get; init; }
    public IExpression Upper { get; init; }
    public IUnaryOperatorLU Operator { get; init; }

    public UnaryExpressionLU(IExpression arg, IExpression lower, IExpression upper, IUnaryOperatorLU @operator)
    {
        Arg = arg;
        Lower = lower;
        Upper = upper;
        Operator = @operator;
    }

    public IExpression GetValue(Context context) =>
        Expr.UnaryLU(Operator, Arg.GetValue(context), Lower.GetValue(context), Upper.GetValue(context));

    public override string ToString() => $"{Operator}_({Lower})^({Upper})({Arg})";
}

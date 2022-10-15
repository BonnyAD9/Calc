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

    public double GetValue() => Operator.Evaluate(Arg.GetValue(), Lower.GetValue(), Upper.GetValue());
}

using calc.Operators;

namespace calc.AST;

internal class BinaryExpression : IExpression
{
    public IExpression Left { get; init; }
    public IExpression Right { get; init; }

    public BinaryOperator Operator { get; init; }

    public BinaryExpression(IExpression left, IExpression right, BinaryOperator @operator)
    {
        Left = left;
        Right = right;
        Operator = @operator;
    }

    public double GetValue() => Operator.Evaluate(Left.GetValue(), Right.GetValue());
}

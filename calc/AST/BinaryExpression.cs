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

    public IExpression GetValue(Context context) =>
        Expr.Binary(Operator, Left.GetValue(context), Right.GetValue(context));

    public override string ToString() => $"({Left}){Operator}({Right})";
}

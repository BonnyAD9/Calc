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

    public IExpression GetValue(IDictionary<string, IExpression> vals) =>
        Expr.Binary(Operator, Left.GetValue(vals), Right.GetValue(vals));

    public override string ToString() => $"({Left}){Operator}({Right})";
}

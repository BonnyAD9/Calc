using calc.Operators;

namespace calc.AST;

internal static class Expr
{
    public static NullExpressoin Null { get; } = new NullExpressoin();

    public static IExpression Constant(double value) => new ConstantExpression(value);

    public static IExpression Unary(IUnaryOperator op, IExpression a) =>
        a is ConstantExpression ac
            ? new ConstantExpression(op.Evaluate(ac.Value))
            : new UnaryExpression(a, op);

    public static IExpression UnaryL(IUnaryOperatorL op, IExpression a, IExpression l) =>
        a is ConstantExpression ac && l is ConstantExpression lc
            ? new ConstantExpression(op.Evaluate(ac.Value, lc.Value))
            : new UnaryExpressionL(a, l, op);

    public static IExpression UnaryU(IUnaryOperatorU op, IExpression a, IExpression u) =>
        a is ConstantExpression ac && u is ConstantExpression uc
            ? new ConstantExpression(op.Evaluate(ac.Value, uc.Value))
            : new UnaryExpressionU(a, u, op);

    public static IExpression UnaryLU(IUnaryOperatorLU op, IExpression a, IExpression l, IExpression u) =>
        a is ConstantExpression ac && l is ConstantExpression lc && u is ConstantExpression uc
            ? new ConstantExpression(op.Evaluate(ac.Value, lc.Value, uc.Value))
            : new UnaryExpressionLU(a, l, u, op);

    public static IExpression Binary(BinaryOperator op, IExpression a, IExpression b) =>
        a is ConstantExpression ac && b is ConstantExpression bc
            ? new ConstantExpression(op.Evaluate(ac.Value, bc.Value))
            : new BinaryExpression(a, b, op);
}

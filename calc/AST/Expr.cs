using calc.Operators;
using System.Linq.Expressions;

namespace calc.AST;

internal static class Expr
{
    public static NullExpressoin Null { get; } = new NullExpressoin();

    public static IExpression Constant(double value) => new ConstantExpression(value);

    public static IExpression Unary(IUnaryOperator op, IExpression a) =>
        a is ConstantExpression
            ? op.Evaluate(a)
            : new UnaryExpression(a, op);

    public static IExpression UnaryL(IUnaryOperatorL op, IExpression a, IExpression l) =>
        a is ConstantExpression && l is ConstantExpression
            ? op.Evaluate(a, l)
            : new UnaryExpressionL(a, l, op);

    public static IExpression UnaryU(IUnaryOperatorU op, IExpression a, IExpression u) =>
        a is ConstantExpression && u is ConstantExpression
            ? op.Evaluate(a, u)
            : new UnaryExpressionU(a, u, op);

    public static IExpression UnaryLU(IUnaryOperatorLU op, IExpression a, IExpression l, IExpression u) =>
        a is ConstantExpression && l is ConstantExpression && u is ConstantExpression
            ? op.Evaluate(a, l, u)
            : new UnaryExpressionLU(a, l, u, op);

    public static IExpression Binary(BinaryOperator op, IExpression a, IExpression b) =>
        a is ConstantExpression && b is ConstantExpression
            ? op.Evaluate(a, b)
            : new BinaryExpression(a, b, op);

    public static IExpression Error(IExpression source, string message) => new ErrorExpression(message);
    public static IExpression Error(IExpression source) => new ErrorExpression();

    public static IExpression Variable(string name) => new VariableExpression(name);

    public static IExpression Postfix(PostfixOperator op, IExpression a) =>
        a is ConstantExpression
            ? op.Evaluate(a)
            : new PostfixExpression(op, a);
}

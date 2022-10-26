using calc.Operators;
using System.Linq.Expressions;

namespace calc.AST;

internal static class Expr
{
    public static NullExpressoin Null { get; } = new NullExpressoin();

    public static IExpression Constant(double value) => new ConstantExpression(value);

    public static IExpression Unary(IUnaryOperator op, IExpression a)
    {
        var e = op.Evaluate(a);
        return e is ErrorExpression ? new UnaryExpression(a, op) : e;
    }

    public static IExpression UnaryL(IUnaryOperatorL op, IExpression a, IExpression l)
    {
        var e = op.Evaluate(a, l);
        return e is ErrorEventArgs ? new UnaryExpressionL(a, l, op) : e;
    }

    public static IExpression UnaryU(IUnaryOperatorU op, IExpression a, IExpression u)
    {
        var e = op.Evaluate(a, u);
        return e is ErrorExpression ? new UnaryExpressionU(a, u, op) : e;
    }

    public static IExpression UnaryLU(IUnaryOperatorLU op, IExpression a, IExpression l, IExpression u)
    {
        var e = op.Evaluate(a, l, u);
        return e is ErrorExpression ? new UnaryExpressionLU(a, l, u, op) : e;
    }

    public static IExpression Binary(BinaryOperator op, IExpression a, IExpression b)
    {
        var e = op.Evaluate(a, b);
        return e is ErrorExpression ? new BinaryExpression(a, b, op) : e;
    }

    public static IExpression Error(IExpression source, string message) => new ErrorExpression(message);
    public static IExpression Error(IExpression source) => new ErrorExpression();

    public static IExpression Variable(string name) => new VariableExpression(name);

    public static IExpression Postfix(PostfixOperator op, IExpression a)
    {
        var e = op.Evaluate(a);
        return e is ErrorExpression ? new PostfixExpression(op, a) : e;
    }
}

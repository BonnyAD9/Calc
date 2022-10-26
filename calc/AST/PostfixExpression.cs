using System.Runtime.CompilerServices;
using calc.Operators;

namespace calc.AST;

internal class PostfixExpression : IExpression
{
    public PostfixOperator Operator { get; init; }
    public IExpression Argument { get; init; }

    public PostfixExpression(PostfixOperator @operator, IExpression argument)
    {
        Operator = @operator;
        Argument = argument;
    }

    public IExpression GetValue(Context context) => Expr.Postfix(Operator, Argument.GetValue(context));
}
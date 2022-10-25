using calc.AST;

namespace calc.Operators;

internal interface IUnaryOperatorU : IUnaryOperator
{
    public IExpression Evaluate(IExpression a, IExpression u);
}

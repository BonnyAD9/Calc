using calc.AST;

namespace calc.Operators;

internal interface IUnaryOperatorL : IUnaryOperator
{
    public IExpression Evaluate(IExpression a, IExpression l);
}

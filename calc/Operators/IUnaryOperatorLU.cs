using calc.AST;

namespace calc.Operators;

internal interface IUnaryOperatorLU : IUnaryOperatorL, IUnaryOperatorU
{
    public IExpression Evaluate(IExpression a, IExpression l, IExpression u);
}

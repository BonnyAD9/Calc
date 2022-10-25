using calc.AST;

namespace calc.Operators;

internal interface IUnaryOperator
{
    public string Name { get; }
    public int Precedence { get; }
    public IExpression Evaluate(IExpression a);
}

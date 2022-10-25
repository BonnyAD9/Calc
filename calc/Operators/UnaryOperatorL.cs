using calc.AST;

namespace calc.Operators;

internal class UnaryOperatorL : IUnaryOperatorL
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly Func<IExpression, IExpression, IExpression> evaluate;

    public UnaryOperatorL(string name, int precedence, Func<IExpression, IExpression, IExpression> evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public IExpression Evaluate(IExpression a) => evaluate(a, Expr.Null);
    public IExpression Evaluate(IExpression a, IExpression l) => evaluate(a, l);

    public override string ToString() => Name;
}

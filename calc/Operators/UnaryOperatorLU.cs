using calc.AST;

namespace calc.Operators;

internal class UnaryOperatorLU : IUnaryOperatorLU
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    readonly Func<IExpression, IExpression, IExpression, IExpression> evaluate;

    public UnaryOperatorLU(string name, int precedence, Func<IExpression, IExpression, IExpression, IExpression> evaluate)
    {
        Name = name;
        Precedence = precedence;
        this.evaluate = evaluate;
    }

    public IExpression Evaluate(IExpression a) => evaluate(a, Expr.Null, Expr.Null);
    IExpression IUnaryOperatorL.Evaluate(IExpression a, IExpression l) => evaluate(a, l, Expr.Null);
    IExpression IUnaryOperatorU.Evaluate(IExpression a, IExpression u) => evaluate(a, Expr.Null, u);
    public IExpression Evaluate(IExpression a, IExpression l, IExpression u) => evaluate(a, l, u);

    public override string ToString() => Name;
}

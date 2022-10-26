using calc.AST;

namespace calc.Operators;

internal class PostfixOperator
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    public Func<IExpression, IExpression> Evaluate { get; init; }

    public PostfixOperator(string name, int precedence, Func<IExpression, IExpression> evaluate)
    {
        Name = name;
        Precedence = precedence;
        Evaluate = evaluate;
    }
}
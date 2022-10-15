namespace calc.AST;

internal interface IExpression
{
    public IExpression GetValue(IDictionary<string, IExpression> vals);
}

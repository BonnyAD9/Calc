namespace calc.AST;

internal interface IExpression
{
    public IExpression GetValue(Context context);
}

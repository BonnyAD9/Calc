namespace calc.AST;

internal class VariableExpression : IExpression
{
    public string Name { get; init; }

    public VariableExpression(string name)
    {
        Name = name;
    }

    public IExpression GetValue(Context context) => context.GetVariableOrDefault(Name, this);

    public override string ToString() => Name;
}

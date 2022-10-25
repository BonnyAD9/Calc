using calc.AST;
using System.Diagnostics.CodeAnalysis;

namespace calc;

internal class Context
{
    private Context? Next { get; }

    private Dictionary<string, IExpression> Variables { get; } = new();

    public Context() { }

    public Context(Context next)
    {
        Next = next;
    }

    public bool HasVariable(string name) => Variables.ContainsKey(name) || (Next is not null && Next.HasVariable(name));

    public IExpression GetVariable(string name) => Variables.GetValueOrDefault(name) ?? Next?.GetVariable(name) ?? throw new IndexOutOfRangeException("The given variable is not present");
    
    [return: NotNullIfNotNull("def")]
    public IExpression? GetVariableOrDefault(string name, IExpression? def = null) => Variables.GetValueOrDefault(name) ?? Next?.GetVariable(name) ?? def;

    public void SetVariable(string name, IExpression value) => Variables[name] = value;
}

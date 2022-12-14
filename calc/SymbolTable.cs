using calc.AST;
using calc.Operators;

namespace calc;

internal class SymbolTable
{
    public Dictionary<string, IExpression> Constants { get; } = new();
    public Dictionary<string, IUnaryOperator> Unary { get; } = new();
    public Dictionary<string, BinaryOperator> Binary { get; } = new();
    public Dictionary<string, PostfixOperator> Postfix { get; } = new();

    public bool HasConstant(string name) => Constants.ContainsKey(name);
    public bool HasUnary(string name) => Unary.ContainsKey(name);
    public bool HasBinary(string name) => Binary.ContainsKey(name);
    public bool HasPostfix(string name) => Postfix.ContainsKey(name);

    public void AddConstants(params (string Name, IExpression Value)[] values)
    {
        foreach (var (name, value) in values)
            Constants[name] = value;
    }

    public void AddUnary(params IUnaryOperator[] values)
    {
        foreach (var v in values)
            Unary[v.Name] = v;
    }

    public void AddBinary(params BinaryOperator[] values)
    {
        foreach (var v in values)
            Binary[v.Name] = v;
    }

    public void AddPostfix(params PostfixOperator[] values)
    {
        foreach (var v in values)
            Postfix[v.Name] = v;
    }
}

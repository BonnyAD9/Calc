using calc.Operators;

namespace calc;

internal class SymbolTable
{
    public Dictionary<string, double> Constants { get; } = new();
    public Dictionary<string, UnaryOperator> Unary { get; } = new();
    public Dictionary<string, BinaryOperator> Binary { get; } = new();

    public bool HasConstant(string name) => Constants.ContainsKey(name);
    public bool HasUnary(string name) => Unary.ContainsKey(name);
    public bool HasBinary(string name) => Binary.ContainsKey(name);

    public void AddConstants(params (string Name, double Value)[] values)
    {
        foreach (var (name, value) in values)
            Constants[name] = value;
    }

    public void AddUnary(params UnaryOperator[] values)
    {
        foreach (var v in values)
            Unary[v.Name] = v;
    }

    public void AddBinary(params BinaryOperator[] values)
    {
        foreach (var v in values)
            Binary[v.Name] = v;
    }
}

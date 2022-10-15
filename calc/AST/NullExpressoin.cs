namespace calc.AST;

internal class NullExpressoin : IExpression
{
    public IExpression GetValue(IDictionary<string, IExpression> vals) => this;

    public static bool operator ==(IExpression l, NullExpressoin r) => l is NullExpressoin;
    public static bool operator !=(IExpression l, NullExpressoin r) => l is not NullExpressoin;

    public override bool Equals(object? obj) => obj is NullExpressoin;

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => "null";
}

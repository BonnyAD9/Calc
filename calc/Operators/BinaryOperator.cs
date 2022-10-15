namespace calc.Operators;

internal class BinaryOperator
{
    public string Name { get; init; }
    public int Precedence { get; init; }
    public Func<double, double, double> Evaluate { get; init; }

    public BinaryOperator(string name, int precedence, Func<double, double, double> evaluate)
    {
        Name = name;
        Precedence = precedence;
        Evaluate = evaluate;
    }

    public static bool operator ==(BinaryOperator l, int r) => l.Precedence == r;
    public static bool operator !=(BinaryOperator l, int r) => l.Precedence != r;
    public static bool operator >(BinaryOperator l, BinaryOperator r) => l.Precedence > r.Precedence;
    public static bool operator <(BinaryOperator l, BinaryOperator r) => l.Precedence < r.Precedence;
    public static bool operator >=(BinaryOperator l, BinaryOperator r) => l.Precedence >= r.Precedence;
    public static bool operator <=(BinaryOperator l, BinaryOperator r) => l.Precedence <= r.Precedence;
    public static bool operator >(BinaryOperator l, int r) => l.Precedence > r;
    public static bool operator <(BinaryOperator l, int r) => l.Precedence < r;
    public static bool operator >=(BinaryOperator l, int r) => l.Precedence >= r;
    public static bool operator <=(BinaryOperator l, int r) => l.Precedence <= r;
    public static bool operator >(int l, BinaryOperator r) => l > r.Precedence;
    public static bool operator <(int l, BinaryOperator r) => l < r.Precedence;
    public static bool operator >=(int l, BinaryOperator r) => l >= r.Precedence;
    public static bool operator <=(int l, BinaryOperator r) => l <= r.Precedence;

    public override bool Equals(object? obj) => obj is BinaryOperator o && o.Name == Name && o.Precedence == Precedence;

    public override int GetHashCode() => Name.GetHashCode() ^ Precedence.GetHashCode();

    public override string ToString() => Name;
}

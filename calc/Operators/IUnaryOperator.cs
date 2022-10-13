namespace calc.Operators;

internal interface IUnaryOperator
{
    public string Name { get; }
    public int Precedence { get; }
    public double Evaluate(double a);
}

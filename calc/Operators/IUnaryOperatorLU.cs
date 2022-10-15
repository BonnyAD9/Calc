namespace calc.Operators;

internal interface IUnaryOperatorLU : IUnaryOperatorL, IUnaryOperatorU
{
    public double Evaluate(double a, double l, double u);
}

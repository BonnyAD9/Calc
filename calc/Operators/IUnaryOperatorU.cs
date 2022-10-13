namespace calc.Operators;

internal interface IUnaryOperatorU : IUnaryOperator
{
    public double Evaluate(double a, double u);
}

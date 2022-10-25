namespace calc.AST;

internal class ErrorExpression : IExpression
{
    public string Message { get; init; } = "";

    public ErrorExpression() { }

    public ErrorExpression(string message)
    {
        Message = message;
    }

    public IExpression GetValue(IDictionary<string, IExpression> vals) => this;

    public override string ToString() => Message;
}

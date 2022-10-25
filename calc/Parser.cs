using calc.AST;
using calc.Operators;

namespace calc;

internal class Parser
{
    Lexer _lexer;
    Token _cur;
    Token Cur => _cur;
    double CurNum => _lexer.NumberToken;
    string CurStr => _lexer.StringToken;
    SymbolTable Symbols { get; init; }
    public Context Context { get; } = new();

    public Parser(Lexer lexer, SymbolTable symbols)
    {
        _lexer = lexer;
        Symbols = symbols;
    }

    public IExpression Parse()
    {
        NextToken();
        return _Parse();
    }

    private IExpression _Parse()
    {
        var e = ParseExpression().GetValue(Context);

        if (Cur != Token.Semicolon)
        {
            if (Cur != Token.Eof)
                Error(Severity.Warning, "Some input at the end has not been consumed");
            return e;
        }

        NextToken();

        if (e is not BinaryExpression be || be.Operator.Name != "=")
            return _Parse();

        if (be.Left is VariableExpression vel)
            Context.SetVariable(vel.Name, be.Right);
        else if (be.Right is VariableExpression ver)
            Context.SetVariable(ver.Name, be.Left);

        if (Cur == Token.Eof)
            return e;

        return _Parse();
    }

    IExpression ParseExpression()
    {
        IExpression expr = ParsePrimaryExpression();
        if (expr == Expr.Null)
            return Expr.Null;

        return ParseBinaryExpression(0, expr);
    }

    IExpression ParsePrimaryExpression()
    {
        switch (Cur)
        {
            case Token.Eof:
                return Expr.Null;
            case Token.Number:
                return ParseNumberExpression();
            case Token.Operator:
                return ParseOperatorExpression();
            case Token.OpenBracket:
                return ParseBracketExpression();
            case Token.OpenAbs:
                return ParseAbsExpression();
            case Token.OpenSet:
                return ParseSetExpression();
            default:
                return Error(Severity.Error, "expected a primary expression");
        }
    }

    IExpression ParseNumberExpression()
    {
        var e = Expr.Constant(CurNum);
        NextToken();
        return e;
    }

    IExpression ParseOperatorExpression()
    {
        if (Symbols.HasUnary(CurStr))
            return ParseUnaryExpression();
;
        var c = Symbols.HasConstant(CurStr) ? Symbols.Constants[CurStr] : Expr.Variable(CurStr);
        NextToken();
        return c;
    }

    IExpression ParseBracketExpression()
    {
        NextToken();
        
        var e = ParseExpression();
        if (e == Expr.Null)
            return Expr.Null;

        if (Cur != Token.CloseBracket)
            return Error(Severity.Error, "Expected ')'");

        NextToken();
        return e;
    }

    IExpression ParseAbsExpression()
    {
        if (!Symbols.HasUnary("abs"))
            return Error(Severity.Error, "abs is not supported");

        NextToken();

        var e = ParseExpression();
        if (e == Expr.Null)
            return Expr.Null;

        if (Cur != Token.CloseAbs)
            return Error(Severity.Error, "Expected ']'");

        NextToken();
        return Expr.Unary(Symbols.Unary["abs"], e);
    }

    IExpression ParseBinaryExpression(int lp)
    {
        var e = ParsePrimaryExpression();
        if (e == Expr.Null)
            return Expr.Null;

        return ParseBinaryExpression(lp, e);
    }

    IExpression ParseBinaryExpression(int lp, IExpression le)
    {
        if (Cur is not Token.Operator and not Token.Upper || !Symbols.HasBinary(CurStr))
            return le;

        while (Cur != Token.Eof && Cur is Token.Operator or Token.Upper && Symbols.HasBinary(CurStr))
        {
            var op = Symbols.Binary[CurStr];

            if (op < lp)
                return le;

            NextToken();
            var re = ParsePrimaryExpression();
            if (re == Expr.Null)
                return Expr.Null;

            if (op < CurPrecedence())
            {
                re = ParseBinaryExpression(op.Precedence, re);
                if (re == Expr.Null)
                    return Expr.Null;
            }

            le = Expr.Binary(op, le, re);
        }

        return le;
    }

    IExpression ParseUnaryExpression()
    {
        if (!Symbols.HasUnary(CurStr))
            return Error(Severity.Error, "Expected a unary operator");

        var op = Symbols.Unary[CurStr];
        NextToken();

        if (Cur == Token.Upper)
            return op is IUnaryOperatorU opu
                ? ParseUnaryUExpression(opu)
                : Error(Severity.Error, $"Operator '{op.Name}' doesn't accept upper index");

        if (Cur == Token.Lower)
            return op is IUnaryOperatorL opl
                ? ParseUnaryLExpression(opl)
                : Error(Severity.Error, $"Operator '{op.Name}' doesn't accept lower index");

        var e = ParseBinaryExpression(op.Precedence);
        if (e == Expr.Null)
            return Expr.Null;

        return Expr.Unary(op, e);
    }

    IExpression ParseUnaryUExpression(IUnaryOperatorU op)
    {
        NextToken();
        
        var u = ParsePrimaryExpression();
        if (u == Expr.Null)
            return Expr.Null;

        if (Cur == Token.Lower)
            return op is IUnaryOperatorLU oplu
                ? ParseUnaryLUExpression(oplu, Expr.Null, u)
                : Error(Severity.Error, $"Operator '{op.Name}' doesn't accept both upper and lower index");

        var a = ParseBinaryExpression(op.Precedence);
        if (a == Expr.Null)
            return Expr.Null;

        return Expr.UnaryU(op, a, u);
    }

    IExpression ParseUnaryLExpression(IUnaryOperatorL op)
    {
        NextToken();

        var l = ParsePrimaryExpression();
        if (l == Expr.Null)
            return Expr.Null;

        if (Cur == Token.Upper)
            return op is IUnaryOperatorLU oplu
                ? ParseUnaryLUExpression(oplu, l, Expr.Null)
                : Error(Severity.Error, $"Operator '{op.Name}' doesn't accept both lower and upper index");

        var a = ParseBinaryExpression(op.Precedence);
        if (a == Expr.Null)
            return Expr.Null;

        return Expr.UnaryL(op, a, l);
    }

    IExpression ParseUnaryLUExpression(IUnaryOperatorLU op, IExpression l, IExpression u)
    {
        NextToken();

        ref IExpression lu = ref u;
        if (l == Expr.Null)
            lu = ref l;

        lu = ParsePrimaryExpression();
        if (lu == Expr.Null)
            return Expr.Null;

        var a = ParseBinaryExpression(op.Precedence);
        if (a == Expr.Null)
            return Expr.Null;

        return Expr.UnaryLU(op, a, l, u);
    }

    IExpression ParseSetExpression()
    {
        return Error(Severity.Error, "Sets are not supported");
    }

    IExpression Error(Severity sev, string message)
    {
        Logger.Instance.Log(message, sev, _lexer.TokenPosition);
        return Expr.Null;
    }

    int CurPrecedence() => Cur is Token.Operator or Token.Upper && Symbols.HasBinary(CurStr) ? Symbols.Binary[CurStr].Precedence : -1;

    Token NextToken() => _cur = _lexer.Next();
}

﻿using calc.AST;
using System.Reflection.Metadata.Ecma335;

namespace calc;

internal class Parser
{
    Lexer _lexer;
    Token _cur;
    Token Cur => _cur;
    double CurNum => _lexer.NumberToken;
    string CurStr => _lexer.StringToken;
    SymbolTable Symbols { get; init; }

    public Parser(Lexer lexer, SymbolTable symbols)
    {
        _lexer = lexer;
        Symbols = symbols;
    }

    public IExpression Parse()
    {
        NextToken();
        return ParseExpression();
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
        ConstantExpression e = new(CurNum);
        NextToken();
        return e;
    }

    IExpression ParseOperatorExpression()
    {
        if (!Symbols.HasConstant(CurStr))
            return ParseUnaryExpression();

        var c = Symbols.Constants[CurStr];
        NextToken();
        return new ConstantExpression(c);
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
        return new UnaryExpression(e, Symbols.Unary["abs"]);
    }

    IExpression ParseBinaryExpression(int lp, IExpression le)
    {
        if (Cur != Token.Operator || !Symbols.HasBinary(CurStr))
            return le;

        while (true)
        {
            if (Cur == Token.Eof)
                return le;

            var op = Symbols.Binary[CurStr];

            if (Cur != Token.Operator || !Symbols.HasBinary(CurStr) || op < lp)
                return le;

            NextToken();
            var re = ParsePrimaryExpression();
            if (re == Expr.Null)
                return Expr.Null;

            if (Cur == Token.Operator && Symbols.HasBinary(CurStr) && op < Symbols.Binary[CurStr])
            {
                re = ParseBinaryExpression(op.Precedence, re);
                if (re == Expr.Null)
                    return Expr.Null;
            }
            
            le = new BinaryExpression(le, re, op);
        }
    }

    IExpression ParseUnaryExpression()
    {
        if (!Symbols.HasUnary(CurStr))
            return Error(Severity.Error, "Expected a unary operator");

        var op = Symbols.Unary[CurStr];
        NextToken();
        
        var e = ParsePrimaryExpression();
        if (e == Expr.Null)
            return Expr.Null;

        return new UnaryExpression(e, op);
    }

    IExpression ParseSetExpression()
    {
        throw new NotImplementedException();
    }

    IExpression Error(Severity sev, string message)
    {
        Console.WriteLine(message);
        return new NullExpressoin();
    }

    Token NextToken() => _cur = _lexer.Next();
}

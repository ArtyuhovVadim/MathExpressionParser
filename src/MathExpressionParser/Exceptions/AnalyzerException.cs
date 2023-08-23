using System;

namespace MathExpressionParser.Exceptions;

public class AnalyzerException : Exception
{
    public AnalyzerException(int position, TokenType[] expectedTokenTypes, Token? unexpectedToken, string message) : base(message)
    {
        Position = position;
        ExpectedTokenTypes = expectedTokenTypes;
        UnexpectedToken = unexpectedToken;
    }

    public int Position { get; }

    public TokenType[] ExpectedTokenTypes { get; }

    public Token? UnexpectedToken { get; }
}
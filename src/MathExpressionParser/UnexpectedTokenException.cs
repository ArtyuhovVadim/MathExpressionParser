using System;

namespace MathExpressionParser;

public class UnexpectedTokenException : Exception
{
    public UnexpectedTokenException(int position, string message) : base(message)
    {
        Position = position;
    }

    public UnexpectedTokenException(int position)
    {
        Position = position;
    }

    public int Position { get; }
}
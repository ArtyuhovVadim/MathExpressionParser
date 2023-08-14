using System;

namespace MathExpressionParser;

public class TokenizeException : Exception
{
    public TokenizeException(int position, string message) : base(message)
    {
        Position = position;
    }

    public int Position { get; }
}
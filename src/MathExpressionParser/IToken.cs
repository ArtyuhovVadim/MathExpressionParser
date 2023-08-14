namespace MathExpressionParser;

public interface IToken
{
    string Text { get; }

    int Position { get; }

    ITokenType Type { get; }
}
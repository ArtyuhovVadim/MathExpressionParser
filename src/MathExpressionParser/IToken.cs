using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser;

public interface IToken
{
    string Text { get; }

    int Position { get; }

    ITokenDefinition Definition { get; }
}
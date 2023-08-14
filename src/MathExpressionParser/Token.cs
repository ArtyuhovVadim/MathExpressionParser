using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser;

public class Token : IToken
{
    public Token(string text, ITokenDefinition definition, int position)
    {
        Text = text;
        Definition = definition;
        Position = position;
    }

    public string Text { get; }

    public int Position { get; }

    public ITokenDefinition Definition { get; }

    public override string ToString() =>
        $"Definition: {{{Definition}}} - Text: '{Text}' - Position: '{Position}'";
}
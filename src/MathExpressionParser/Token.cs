namespace MathExpressionParser;

public class Token : IToken
{
    public Token(string text, ITokenType type, int position)
    {
        Text = text;
        Type = type;
        Position = position;
    }

    public string Text { get; }

    public int Position { get; }

    public ITokenType Type { get; }

    public override string ToString() =>
        $"Type: {{{Type}}} - Text: '{Text}' - Position: '{Position}'";
}
namespace MathExpressionParser;

public class Token
{
    public Token(string text, TokenType type, int position)
    {
        Text = text;
        Type = type;
        Position = position;
    }

    public string Text { get; }

    public int Position { get; }

    public TokenType Type { get; }

    public override string ToString() =>
        $$"""{ Type: "{{Type}}", Position: {{Position}}, Text: "{{Text}}" }""";
}
using System.Text.RegularExpressions;

namespace MathExpressionParser;

public class TokenType : ITokenType
{
    public TokenType(string name, string regex)
    {
        Name = name;
        Regex = new Regex(@"\G" + regex, RegexOptions.Compiled);
    }

    public string Name { get; }

    public Regex Regex { get; }

    public override string ToString() =>
        $"Name: '{Name}' - Regex: '{Regex}'";
}
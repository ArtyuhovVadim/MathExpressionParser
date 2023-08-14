using System.Collections.Generic;

namespace MathExpressionParser;

public class MathParser
{
    private readonly List<ITokenType> _availableTokenTypes = new()
    {
        new TokenType("Space", @"\s"),

        new TokenType("Number", @"[+-]?(\d*\.\d+|\d+\.\d*|\d+)"),

        new TokenType("Plus", @"\+"),
        new TokenType("Minus", @"\-"),
        new TokenType("Multiply", @"\*"),
        new TokenType("Divide", @"\/"),
    };

    public double Parse(string input)
    {
        var tokens = GenerateTokens(input);

        return double.NaN;
    }

    private List<IToken> GenerateTokens(string input)
    {
        var tokens = new List<IToken>();
        var position = 0;

        while (position < input.Length)
        {
            var matched = false;

            foreach (var tokenType in _availableTokenTypes)
            {
                var match = tokenType.Regex.Match(input, position);

                if (!match.Success) continue;

                tokens.Add(new Token(match.Value, tokenType, position));

                position += match.Length;
                matched = true;
                break;
            }

            if (!matched)
                throw new UnexpectedTokenException(position, "Unable to parse token");
        }

        return tokens;
    }
}
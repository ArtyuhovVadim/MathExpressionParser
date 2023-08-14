using System.Collections.Generic;
using System.Linq;
using MathExpressionParser.TokenDefinitions;
using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser;

public class MathParser
{
    private readonly List<ITokenDefinition> _tokenDefinitions = new()
    {
        new SpaceTokenDefinition(),

        new NumberTokenDefinition(),

        new PlusTokenDefinition(),
        new MinusTokenDefinition(),
        new MultiplyTokenDefinition(),
        new DivideTokenDefinition()
    };

    public double Parse(string input)
    {
        var tokens = GenerateTokens(input).Where(x => x.Definition is not SpaceTokenDefinition).ToList();

        return double.NaN;
    }

    private List<IToken> GenerateTokens(string input)
    {
        var tokens = new List<IToken>();
        var position = 0;

        while (position < input.Length)
        {
            var matched = false;

            foreach (var tokenDefinition in _tokenDefinitions)
            {
                var match = tokenDefinition.Regex.Match(input, position);

                if (!match.Success) continue;

                tokens.Add(new Token(match.Value, tokenDefinition, position));

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
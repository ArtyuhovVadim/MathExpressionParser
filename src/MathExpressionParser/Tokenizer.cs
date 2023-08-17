using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MathExpressionParser;

public class Tokenizer
{
    private static readonly Dictionary<TokenType, Regex> RegexMap = new()
    {
        { TokenType.Number, new Regex(@"\G((\d*\.\d+e\d+|\d+\.\d*e\d+|\d+e\d+)|(\d*\.\d+|\d+\.\d*|\d+))") },
        { TokenType.Mod, new Regex(@"\G\%") },
        { TokenType.Div, new Regex(@"\G\/\/") },
        { TokenType.Function, new Regex(@"\G[a-zA-Zа-яА-Я_]+[a-zA-Zа-яА-Я_0-9]+\(") },
        { TokenType.Comma, new Regex(@"\G\,") },
        { TokenType.Constant, new Regex(@"\G[a-zA-Zа-яА-Я]+") },
        { TokenType.Plus, new Regex(@"\G\+") },
        { TokenType.Minus, new Regex(@"\G\-") },
        { TokenType.Divide, new Regex(@"\G\/") },
        { TokenType.Multiply, new Regex(@"\G\*") },
        { TokenType.Degree, new Regex(@"\G\^") },
        { TokenType.LeftBracket, new Regex(@"\G\(") },
        { TokenType.RightBracket, new Regex(@"\G\)") }
    };

    public IEnumerable<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();
        var position = 0;

        while (position < input.Length)
        {
            var matched = false;

            if (char.IsWhiteSpace(input[position]))
            {
                position++;
                continue;
            }

            foreach (var (type, regex) in RegexMap)
            {
                var match = regex.Match(input, position);

                if (!match.Success) continue;

                tokens.Add(new Token(match.Value, type, position));

                position += match.Length;
                matched = true;

                break;
            }

            if (!matched)
                throw new TokenizeException(position, $"Undefined token type at position {position}.");
        }

        tokens.Add(new Token(string.Empty, TokenType.End, -1));

        return tokens;
    }
}
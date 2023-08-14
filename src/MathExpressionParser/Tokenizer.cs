﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MathExpressionParser;

public class Tokenizer
{
    private readonly Dictionary<TokenType, Regex> _regexMap = new()
    {
        { TokenType.Space, new Regex(@"\G\s") },
        { TokenType.Number, new Regex(@"\G[+-]?(\d*\.\d+|\d+\.\d*|\d+)") },
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

            foreach (var (type, regex) in _regexMap)
            {
                var match = regex.Match(input, position);

                if (!match.Success) continue;

                tokens.Add(new Token(match.Value, type, position));

                position += match.Length;
                matched = true;
                break;
            }

            if (!matched)
                throw new TokenizeException(position, $"Undefined token at {position} position.");
        }

        tokens.Add(new Token(string.Empty, TokenType.End, -1));

        return tokens;
    }
}
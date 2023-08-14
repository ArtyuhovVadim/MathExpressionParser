using System.Text.RegularExpressions;

namespace MathExpressionParser;

public interface ITokenType
{
    string Name { get; }

    Regex Regex { get; }
}
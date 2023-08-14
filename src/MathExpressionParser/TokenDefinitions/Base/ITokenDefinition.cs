using System.Text.RegularExpressions;

namespace MathExpressionParser.TokenDefinitions.Base;

public interface ITokenDefinition
{
    string Name { get; }

    Regex Regex { get; }
}
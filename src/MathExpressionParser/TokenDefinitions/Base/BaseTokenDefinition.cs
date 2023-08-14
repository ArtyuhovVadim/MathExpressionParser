using System.Text.RegularExpressions;

namespace MathExpressionParser.TokenDefinitions.Base;

public abstract class BaseTokenDefinition : ITokenDefinition
{
    protected BaseTokenDefinition(string name, string regex)
    {
        Name = name;
        Regex = new Regex(@"\G" + regex, RegexOptions.Compiled);
    }

    public string Name { get; }

    public Regex Regex { get; }

    public override string ToString() =>
        $"Name: '{Name}' - Regex: '{Regex}'";
}
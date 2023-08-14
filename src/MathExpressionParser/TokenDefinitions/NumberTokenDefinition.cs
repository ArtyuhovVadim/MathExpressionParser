using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser.TokenDefinitions;

public class NumberTokenDefinition : BaseTokenDefinition
{
    public NumberTokenDefinition() : base(nameof(NumberTokenDefinition), @"[+-]?(\d*\.\d+|\d+\.\d*|\d+)") { }
}
using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser.TokenDefinitions;

public class MinusTokenDefinition : BaseTokenDefinition
{
    public MinusTokenDefinition() : base(nameof(MinusTokenDefinition), @"\-") { }
}
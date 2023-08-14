using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser.TokenDefinitions;

public class SpaceTokenDefinition : BaseTokenDefinition
{
    public SpaceTokenDefinition() : base(nameof(SpaceTokenDefinition), @"\s") { }
}
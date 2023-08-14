using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser.TokenDefinitions;

public class PlusTokenDefinition : BaseTokenDefinition
{
    public PlusTokenDefinition() : base(nameof(PlusTokenDefinition), @"\+") { }
}
using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser.TokenDefinitions;

public class MultiplyTokenDefinition : BaseTokenDefinition
{
    public MultiplyTokenDefinition() : base(nameof(MultiplyTokenDefinition), @"\*") { }
}
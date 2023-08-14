using MathExpressionParser.TokenDefinitions.Base;

namespace MathExpressionParser.TokenDefinitions;

public class DivideTokenDefinition : BaseTokenDefinition
{
    public DivideTokenDefinition() : base(nameof(DivideTokenDefinition), @"\/") { }
}
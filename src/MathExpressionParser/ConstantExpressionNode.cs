using System;
using System.Collections.Generic;

namespace MathExpressionParser;

public class ConstantExpressionNode : ExpressionTreeNode
{
    private readonly Dictionary<string, double> _constantsMap = new()
    {
        { "pi", Math.PI },
        { "e",  Math.E  },
    };

    public ConstantExpressionNode(Token token) : base(token) { }

    public override double Evaluate()
    {
        if (_constantsMap.TryGetValue(Token.Text.ToLower(), out var value))
        {
            return value;
        }

        throw new KeyNotFoundException($"Can not evaluate '{Token.Text}' constant.");
    }

    public override string ToString() => Token.Text;
}
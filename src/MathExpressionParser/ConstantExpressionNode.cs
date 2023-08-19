using System;
using System.Collections.Generic;

namespace MathExpressionParser;

public class ConstantExpressionNode : ExpressionTreeNode
{
    //TODO: Вынести для более удобного изменения/добавления новых элементов.
    private static readonly Dictionary<string, double> ConstantsMap = new()
    {
        { "pi", Math.PI },
        { "e",  Math.E  },
        { "max",  double.MaxValue  },
        { "min",  double.MinValue  },
    };

    public ConstantExpressionNode(Token token) : base(token) { }

    public override double Evaluate()
    {
        if (ConstantsMap.TryGetValue(Token.Text.ToLower(), out var value))
        {
            return value;
        }

        throw new KeyNotFoundException($"Can not evaluate '{Token.Text}' constant.");
    }

    public override string ToString() => Token.Text;
}
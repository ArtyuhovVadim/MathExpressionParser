using System;
using System.Globalization;

namespace MathExpressionParser;

public class NumberTreeNode : ExpressionTreeNode
{
    public NumberTreeNode(Token token) : base(token) { }

    public override double Evaluate()
    {
        if (Token.Text.Length > 2 && Token.Text[0] == '0' && char.ToLower(Token.Text[1]) == 'b')
        {
            return Convert.ToInt32(Token.Text[2..], 2);
        }

        if (Token.Text.Length > 2 && Token.Text[0] == '0' && char.ToLower(Token.Text[1]) == 'o')
        {
            return Convert.ToInt32(Token.Text[2..], 8);
        }

        if (Token.Text.Length > 2 && Token.Text[0] == '0' && char.ToLower(Token.Text[1]) == 'x')
        {
            return Convert.ToInt32(Token.Text[2..], 16);
        }

        return double.Parse(Token.Text, CultureInfo.InvariantCulture);
    }

    public override string ToString() => Token.Text;
}
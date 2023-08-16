using System.Globalization;

namespace MathExpressionParser;

public class NumberTreeNode : ExpressionTreeNode
{
    public NumberTreeNode(Token token) : base(token) { }

    public override double Evaluate() => double.Parse(Token.Text, CultureInfo.InvariantCulture);

    public override string ToString() => Token.Text;
}
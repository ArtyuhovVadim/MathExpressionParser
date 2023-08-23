using MathExpressionParser.ExpressionNodes.Base;

namespace MathExpressionParser.ExpressionNodes;

public class ConstantTreeNode : ExpressionTreeNode
{
    public ConstantTreeNode(Token token) : base(token) { }

    public override double Evaluate(Evaluator evaluator) => evaluator.Evaluate(this);

    public override string ToString() => Token.Text;
}
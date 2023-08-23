namespace MathExpressionParser;

public class ConstantExpressionNode : ExpressionTreeNode
{
    public ConstantExpressionNode(Token token) : base(token) { }

    public override double Evaluate(Evaluator evaluator) => evaluator.Evaluate(this);

    public override string ToString() => Token.Text;
}
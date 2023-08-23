namespace MathExpressionParser;

public class NumberTreeNode : ExpressionTreeNode
{
    public NumberTreeNode(Token token) : base(token) { }

    public override double Evaluate(Evaluator evaluator) => evaluator.Evaluate(this);

    public override string ToString() => Token.Text;
}
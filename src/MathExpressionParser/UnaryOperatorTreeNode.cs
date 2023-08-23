namespace MathExpressionParser;

public class UnaryOperatorTreeNode : ExpressionTreeNode
{
    public ExpressionTreeNode Operand { get; }

    public UnaryOperatorTreeNode(Token token, ExpressionTreeNode operand) : base(token) => Operand = operand;

    public override double Evaluate(Evaluator evaluator) => evaluator.Evaluate(this);

    public override string ToString() => $"[{Token.Text}{Operand.Token.Text}]";
}
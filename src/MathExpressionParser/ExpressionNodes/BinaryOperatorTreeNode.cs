using MathExpressionParser.ExpressionNodes.Base;

namespace MathExpressionParser.ExpressionNodes;

public class BinaryOperatorTreeNode : ExpressionTreeNode
{
    public ExpressionTreeNode OperandA { get; }

    public ExpressionTreeNode OperandB { get; }

    public BinaryOperatorTreeNode(Token token, ExpressionTreeNode operandA, ExpressionTreeNode operandB) : base(token)
    {
        OperandA = operandA;
        OperandB = operandB;
    }

    public override double Evaluate(Evaluator evaluator) => evaluator.Evaluate(this);

    public override string ToString() => $"[{OperandA} {Token.Text} {OperandB}]";
}
namespace MathExpressionParser.ExpressionNodes.Base;

public abstract class ExpressionTreeNode
{
    public Token Token { get; }

    protected ExpressionTreeNode(Token token) => Token = token;

    public abstract double Evaluate(Evaluator evaluator);
}
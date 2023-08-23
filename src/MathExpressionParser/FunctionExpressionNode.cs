using System.Collections.Generic;

namespace MathExpressionParser;

public class FunctionExpressionNode : ExpressionTreeNode
{
    private readonly List<ExpressionTreeNode> _arguments;

    public FunctionExpressionNode(Token token, List<ExpressionTreeNode> arguments) : base(token) => _arguments = arguments;

    public IReadOnlyList<ExpressionTreeNode> Arguments => _arguments;

    public override double Evaluate(Evaluator evaluator) => evaluator.Evaluate(this);

    public override string ToString() => $"{Token.Text}{string.Join(", ", _arguments)})";
}
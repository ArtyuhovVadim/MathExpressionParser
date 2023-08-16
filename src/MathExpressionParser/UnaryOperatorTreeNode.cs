using System;

namespace MathExpressionParser;

public class UnaryOperatorTreeNode : ExpressionTreeNode
{
    public ExpressionTreeNode Operand { get; }

    public UnaryOperatorTreeNode(Token token, ExpressionTreeNode operand) : base(token) => Operand = operand;

    public override double Evaluate() => Token.Type switch
    {
        TokenType.Minus => -Operand.Evaluate(),
        TokenType.Plus => +Operand.Evaluate(),
        _ => throw new ArgumentOutOfRangeException()
    };

    public override string ToString() => $"[{Token.Text}{Operand.Token.Text}]";
}
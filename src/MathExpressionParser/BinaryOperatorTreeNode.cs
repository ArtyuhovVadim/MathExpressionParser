using System;

namespace MathExpressionParser;

public class BinaryOperatorTreeNode : ExpressionTreeNode
{
    public ExpressionTreeNode OperandA { get; }

    public ExpressionTreeNode OperandB { get; }

    public BinaryOperatorTreeNode(Token token, ExpressionTreeNode operandA, ExpressionTreeNode operandB) : base(token)
    {
        OperandA = operandA;
        OperandB = operandB;
    }

    public override double Evaluate() => Token.Type switch
    {
        TokenType.Minus => OperandA.Evaluate() - OperandB.Evaluate(),
        TokenType.Plus => OperandA.Evaluate() + OperandB.Evaluate(),
        TokenType.Multiply => OperandA.Evaluate() * OperandB.Evaluate(),
        TokenType.Divide => OperandA.Evaluate() / OperandB.Evaluate(),
        TokenType.Degree => Math.Pow(OperandA.Evaluate(), OperandB.Evaluate()),
        _ => throw new ArgumentOutOfRangeException()
    };

    public override string ToString() => $"[{OperandA} {Token.Text} {OperandB}]";
}
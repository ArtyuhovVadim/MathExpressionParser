using System;
using System.Collections.Generic;

namespace MathExpressionParser;

public class FunctionExpressionNode : ExpressionTreeNode
{
    private readonly List<ExpressionTreeNode> _arguments;

    public FunctionExpressionNode(Token token, List<ExpressionTreeNode> arguments) : base(token)
    {
        _arguments = arguments;
    }

    public override double Evaluate()
    {
        var functionName = Token.Text.ToLower().TrimEnd('(');

        return functionName switch
        {
            "sin" => Math.Sin(_arguments[0].Evaluate()),
            "cos" => Math.Cos(_arguments[0].Evaluate()),
            "pow" => Math.Pow(_arguments[0].Evaluate(), _arguments[1].Evaluate()),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override string ToString() => $"{Token.Text}{string.Join(", ", _arguments)})";
}
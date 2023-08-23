using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using MathExpressionParser.ExpressionNodes;

namespace MathExpressionParser;

public class Evaluator
{
    private readonly Dictionary<string, FunctionDefinition> _functionsMap;

    private readonly Dictionary<string, ConstantDefinition> _constantsMap;

    public Evaluator(IEnumerable<FunctionDefinition> functionDefinitions, IEnumerable<ConstantDefinition> constantDefinitions)
    {
        _functionsMap = functionDefinitions.ToDictionary(key => key.Name, value => value);
        _constantsMap = constantDefinitions.ToDictionary(key => key.Name, value => value);
    }

    public double Evaluate(NumberTreeNode node)
    {
        var text = node.Token.Text;

        if (text.Length > 2 && text[0] == '0' && char.ToLower(text[1]) == 'b')
        {
            return Convert.ToInt32(text[2..], 2);
        }

        if (text.Length > 2 && text[0] == '0' && char.ToLower(text[1]) == 'o')
        {
            return Convert.ToInt32(text[2..], 8);
        }

        if (text.Length > 2 && text[0] == '0' && char.ToLower(text[1]) == 'x')
        {
            return Convert.ToInt32(text[2..], 16);
        }

        return double.Parse(text, CultureInfo.InvariantCulture);
    }

    public double Evaluate(BinaryOperatorTreeNode node) => node.Token.Type switch
    {
        TokenType.Minus => node.OperandA.Evaluate(this) - node.OperandB.Evaluate(this),
        TokenType.Plus => node.OperandA.Evaluate(this) + node.OperandB.Evaluate(this),
        TokenType.Multiply => node.OperandA.Evaluate(this) * node.OperandB.Evaluate(this),
        TokenType.Divide => node.OperandA.Evaluate(this) / node.OperandB.Evaluate(this),
        TokenType.Mod => node.OperandA.Evaluate(this) % node.OperandB.Evaluate(this),
        TokenType.Div => (long)node.OperandA.Evaluate(this) / (long)node.OperandB.Evaluate(this),
        TokenType.Degree => Math.Pow(node.OperandA.Evaluate(this), node.OperandB.Evaluate(this)),
        _ => throw new ArgumentOutOfRangeException()
    };

    public double Evaluate(UnaryOperatorTreeNode node) => node.Token.Type switch
    {
        TokenType.Minus => -node.Operand.Evaluate(this),
        TokenType.Plus => +node.Operand.Evaluate(this),
        _ => throw new ArgumentOutOfRangeException()
    };

    public double Evaluate(FunctionTreeNode node)
    {
        var functionName = node.Token.Text.ToLower().TrimEnd('(');

        if (_functionsMap.TryGetValue(functionName, out var value))
        {
            return value.Evaluate(node.Arguments, this);
        }

        throw new KeyNotFoundException($"Can not evaluate '{functionName}' function.");
    }

    public double Evaluate(ConstantTreeNode node)
    {
        if (_constantsMap.TryGetValue(node.Token.Text.ToLower(), out var constantDefinition))
        {
            return constantDefinition.Value;
        }

        throw new KeyNotFoundException($"Can not evaluate '{node.Token.Text}' constant.");
    }
}
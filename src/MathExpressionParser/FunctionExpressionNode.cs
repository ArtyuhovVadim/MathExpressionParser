using System;
using System.Collections.Generic;

namespace MathExpressionParser;

public class FunctionExpressionNode : ExpressionTreeNode
{
    //TODO: Вынести для более удобного изменения/добавления новых элементов.
    private static readonly Dictionary<string, FunctionDefinition> FunctionsMap = new()
    {
        { "sin", new FunctionDefinition("sin", 1, args => Math.Sin(args[0])) },
        { "cos", new FunctionDefinition("cos", 1, args => Math.Cos(args[0])) },
        { "pow", new FunctionDefinition("pow", 2, args => Math.Pow(args[0], args[1])) },
    };

    private readonly List<ExpressionTreeNode> _arguments;

    public FunctionExpressionNode(Token token, List<ExpressionTreeNode> arguments) : base(token) => _arguments = arguments;

    public override double Evaluate()
    {
        var functionName = Token.Text.ToLower().TrimEnd('(');

        if (FunctionsMap.TryGetValue(functionName, out var value))
        {
            return value.Evaluate(_arguments);
        }

        throw new KeyNotFoundException($"Can not evaluate '{functionName}' function.");
    }

    public override string ToString() => $"{Token.Text}{string.Join(", ", _arguments)})";
}
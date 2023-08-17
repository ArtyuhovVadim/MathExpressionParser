using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExpressionParser;

public class FunctionExpressionNode : ExpressionTreeNode
{
    //TODO: Вынести для более удобного изменения/добавления новых элементов.
    private static readonly Dictionary<string, FunctionDefinition> FunctionsMap = new()
    {
        { "sin",  new FunctionDefinition("sin",  1,  args => Math.Sin(args[0]))          },
        { "asin", new FunctionDefinition("asin", 1,  args => Math.Asin(args[0]))         },
        { "cos",  new FunctionDefinition("cos",  1,  args => Math.Cos(args[0]))          },
        { "acos", new FunctionDefinition("acos", 1,  args => Math.Acos(args[0]))         },
        { "tan",  new FunctionDefinition("tan",  1,  args => Math.Tan(args[0]))          },
        { "atan", new FunctionDefinition("atan", 1,  args => Math.Atan(args[0]))         },
        { "pow",  new FunctionDefinition("pow",  2,  args => Math.Pow(args[0], args[1])) },
        { "abs",  new FunctionDefinition("abs",  1,  args => Math.Abs(args[0]))          },
        { "sqrt", new FunctionDefinition("sqrt", 1,  args => Math.Sqrt(args[0]))         },
        { "log",  new FunctionDefinition("log",  2,  args => Math.Log(args[0], args[1])) },
        { "lg",   new FunctionDefinition("lg",   1,  args => Math.Log10(args[0]))        },
        { "ln",   new FunctionDefinition("ln",   1,  args => Math.Log(args[0]))          },
        { "exp",  new FunctionDefinition("exp",  1,  args => Math.Exp(args[0]))          },
        { "max",  new FunctionDefinition("max",  -1, args => args.Max())                 },
        { "min",  new FunctionDefinition("min",  -1, args => args.Min())                 },
        { "sign", new FunctionDefinition("sign", 1,  args => Math.Sign(args[0]))         },
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
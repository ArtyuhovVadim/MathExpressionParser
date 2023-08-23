using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using MathExpressionParser.ExpressionNodes;

namespace MathExpressionParser;

public class Evaluator
{
    private static readonly Dictionary<string, FunctionDefinition> FunctionsMap = new()
    {
        { "sin",    new FunctionDefinition("sin",    1,  args => Math.Sin(args[0]))                 },
        { "arcsin", new FunctionDefinition("arcsin", 1,  args => Math.Asin(args[0]))                },
        { "cos",    new FunctionDefinition("cos",    1,  args => Math.Cos(args[0]))                 },
        { "arccos", new FunctionDefinition("arccos", 1,  args => Math.Acos(args[0]))                },
        { "tg",     new FunctionDefinition("tg",     1,  args => Math.Tan(args[0]))                 },
        { "arctg",  new FunctionDefinition("arctg",  1,  args => Math.Atan(args[0]))                },
        { "ctg",    new FunctionDefinition("ctg",    1,  args => 1d / Math.Tan(args[0]))            },
        { "arcctg", new FunctionDefinition("arcctg", 1,  args => Math.PI / 2d - Math.Atan(args[0])) },
        { "pow",    new FunctionDefinition("pow",    2,  args => Math.Pow(args[0], args[1]))        },
        { "abs",    new FunctionDefinition("abs",    1,  args => Math.Abs(args[0]))                 },
        { "sqrt",   new FunctionDefinition("sqrt",   1,  args => Math.Sqrt(args[0]))                },
        { "log",    new FunctionDefinition("log",    2,  args => Math.Log(args[0], args[1]))        },
        { "lg",     new FunctionDefinition("lg",     1,  args => Math.Log10(args[0]))               },
        { "ln",     new FunctionDefinition("ln",     1,  args => Math.Log(args[0]))                 },
        { "exp",    new FunctionDefinition("exp",    1,  args => Math.Exp(args[0]))                 },
        { "sign",   new FunctionDefinition("sign",   1,  args => Math.Sign(args[0]))                },
        { "d2r",    new FunctionDefinition("d2r",    1,  args => args[0] * Math.PI / 180d)          },
        { "r2d",    new FunctionDefinition("r2d",    1,  args => args[0] * 180d / Math.PI)          },
        { "max",    new FunctionDefinition("max", argsCount => argsCount > 1, args => args.Max())     },
        { "min",    new FunctionDefinition("min", argsCount => argsCount > 1, args => args.Min())     },
        { "avg",    new FunctionDefinition("avg", argsCount => argsCount > 1, args => args.Average()) },
    };

    private static readonly Dictionary<string, double> ConstantsMap = new()
    {
        { "pi", Math.PI },
        { "e",  Math.E  },
        { "max",  double.MaxValue  },
        { "min",  double.MinValue  },
    };

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

        if (FunctionsMap.TryGetValue(functionName, out var value))
        {
            return value.Evaluate(node.Arguments, this);
        }

        throw new KeyNotFoundException($"Can not evaluate '{functionName}' function.");
    }

    public double Evaluate(ConstantTreeNode node)
    {
        if (ConstantsMap.TryGetValue(node.Token.Text.ToLower(), out var value))
        {
            return value;
        }

        throw new KeyNotFoundException($"Can not evaluate '{node.Token.Text}' constant.");
    }
}
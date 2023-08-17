using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExpressionParser;

public class FunctionDefinition
{
    private readonly Func<double[], double> _func;

    public FunctionDefinition(string name, int argumentsCount, Func<double[], double> func)
    {
        _func = func;
        Name = name;
        ArgumentsCount = argumentsCount;
    }

    public string Name { get; }

    public int ArgumentsCount { get; }

    public double Evaluate(List<ExpressionTreeNode> args)
    {
        if (ArgumentsCount == -1 && args.Count == 0)
        {
            throw new ArgumentException($"The number of arguments for the '{Name}' function must be greater than zero.");
        }
        
        if (ArgumentsCount != -1 && args.Count != ArgumentsCount)
        {
            throw new ArgumentException($"Too many or too few arguments for the '{Name}' function.");
        }

        return _func(args.Select(x => x.Evaluate()).ToArray());
    }
}
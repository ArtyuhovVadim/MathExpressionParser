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
        if (args.Count != ArgumentsCount)
            throw new ArgumentException($"Too many or too few arguments for '{Name}' function");

        return _func(args.Select(x => x.Evaluate()).ToArray());
    }
}
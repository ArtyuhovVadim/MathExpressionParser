﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExpressionParser;

public class FunctionDefinition
{
    private readonly Func<double[], double> _func;
    private readonly Predicate<int> _argsCountValidator;

    public FunctionDefinition(string name, Predicate<int> argsCountValidator, Func<double[], double> func)
    {
        _func = func;
        Name = name;
        _argsCountValidator = argsCountValidator;
    }

    public FunctionDefinition(string name, int argumentsCount, Func<double[], double> func)
    {
        _func = func;
        Name = name;
        _argsCountValidator = argsCount => argsCount == argumentsCount;
    }

    public string Name { get; }

    public double Evaluate(List<ExpressionTreeNode> args)
    {
        if(!_argsCountValidator(args.Count))
            throw new ArgumentException($"Invalid arguments count for the '{Name}' function.");

        return _func(args.Select(x => x.Evaluate()).ToArray());
    }
}
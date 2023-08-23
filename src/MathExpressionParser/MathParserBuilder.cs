using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExpressionParser;

public class MathParserBuilder
{
    private readonly List<ConstantDefinition> _constantDefinitions = new();

    private readonly List<FunctionDefinition> _functionsDefinitions = new();

    private MathParserBuilder() { }

    public static MathParserBuilder Create() => new();

    public static MathParser BuildDefaultParser() =>
         Create()
        .WithDefaultConstants()
        .WithDefaultFunctions()
        .Build();

    public MathParser Build() => new(_functionsDefinitions, _constantDefinitions);

    public MathParserBuilder WithConstant(string name, double value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Name must be not null or white space.");

        if (_constantDefinitions.Any(x => x.Name == name))
            throw new InvalidOperationException($"Constant '{name}' already exists.");

        _constantDefinitions.Add(new ConstantDefinition(name, value));
        return this;
    }

    public MathParserBuilder WithFunction(string name, int argsCount, Func<double[], double> func)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Name must be not null or white space.");

        if (argsCount < 0)
            throw new ArgumentOutOfRangeException(nameof(argsCount), "Argument count must be greater than or equals to zero.");

        if (_functionsDefinitions.Any(x => x.Name == name))
            throw new InvalidOperationException($"Function '{name}' already exists.");

        _functionsDefinitions.Add(new FunctionDefinition(name, argsCount, func));
        return this;
    }

    public MathParserBuilder WithFunction(string name, Predicate<int> argsCountValidator, Func<double[], double> func)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Name must be not null or white space.");

        if (_functionsDefinitions.Any(x => x.Name == name))
            throw new InvalidOperationException($"Function '{name}' already exists.");

        _functionsDefinitions.Add(new FunctionDefinition(name, argsCountValidator, func));
        return this;
    }

    public MathParserBuilder WithDefaultConstants() =>
         WithConstant("pi", Math.PI)
        .WithConstant("e", Math.E)
        .WithConstant("max", double.MaxValue)
        .WithConstant("min", double.MinValue);

    public MathParserBuilder WithDefaultFunctions() =>
         WithFunction("sin", 1, args => Math.Sin(args[0]))
        .WithFunction("arcsin", 1, args => Math.Asin(args[0]))
        .WithFunction("cos", 1, args => Math.Cos(args[0]))
        .WithFunction("arccos", 1, args => Math.Acos(args[0]))
        .WithFunction("tg", 1, args => Math.Tan(args[0]))
        .WithFunction("arctg", 1, args => Math.Atan(args[0]))
        .WithFunction("ctg", 1, args => 1d / Math.Tan(args[0]))
        .WithFunction("arcctg", 1, args => Math.PI / 2d - Math.Atan(args[0]))
        .WithFunction("pow", 2, args => Math.Pow(args[0], args[1]))
        .WithFunction("abs", 1, args => Math.Abs(args[0]))
        .WithFunction("sqrt", 1, args => Math.Sqrt(args[0]))
        .WithFunction("log", 2, args => Math.Log(args[0], args[1]))
        .WithFunction("lg", 1, args => Math.Log10(args[0]))
        .WithFunction("ln", 1, args => Math.Log(args[0]))
        .WithFunction("exp", 1, args => Math.Exp(args[0]))
        .WithFunction("sign", 1, args => Math.Sign(args[0]))
        .WithFunction("d2r", 1, args => args[0] * Math.PI / 180d)
        .WithFunction("r2d", 1, args => args[0] * 180d / Math.PI)
        .WithFunction("max", argsCount => argsCount > 1, args => args.Max())
        .WithFunction("min", argsCount => argsCount > 1, args => args.Min())
        .WithFunction("avg", argsCount => argsCount > 1, args => args.Average());
}
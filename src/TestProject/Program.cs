using MathExpressionParser;

List<FunctionDefinition> functions = new()
{
    new FunctionDefinition("sin",    1,  args => Math.Sin(args[0])),
    new FunctionDefinition("arcsin", 1,  args => Math.Asin(args[0])),
    new FunctionDefinition("cos",    1,  args => Math.Cos(args[0])),
    new FunctionDefinition("arccos", 1,  args => Math.Acos(args[0])),
    new FunctionDefinition("tg",     1,  args => Math.Tan(args[0])),
    new FunctionDefinition("arctg",  1,  args => Math.Atan(args[0])),
    new FunctionDefinition("ctg",    1,  args => 1d / Math.Tan(args[0])),
    new FunctionDefinition("arcctg", 1,  args => Math.PI / 2d - Math.Atan(args[0])),
    new FunctionDefinition("pow",    2,  args => Math.Pow(args[0], args[1])),
    new FunctionDefinition("abs",    1,  args => Math.Abs(args[0])),
    new FunctionDefinition("sqrt",   1,  args => Math.Sqrt(args[0])),
    new FunctionDefinition("log",    2,  args => Math.Log(args[0], args[1])),
    new FunctionDefinition("lg",     1,  args => Math.Log10(args[0])),
    new FunctionDefinition("ln",     1,  args => Math.Log(args[0])),
    new FunctionDefinition("exp",    1,  args => Math.Exp(args[0])),
    new FunctionDefinition("sign",   1,  args => Math.Sign(args[0])),
    new FunctionDefinition("d2r",    1,  args => args[0] * Math.PI / 180d),
    new FunctionDefinition("r2d",    1,  args => args[0] * 180d / Math.PI),
    new FunctionDefinition("max", argsCount => argsCount > 1, args => args.Max()),
    new FunctionDefinition("min", argsCount => argsCount > 1, args => args.Min()),
    new FunctionDefinition("avg", argsCount => argsCount > 1, args => args.Average()),
};

List<ConstantDefinition> constants = new()
{
    new ConstantDefinition("pi", Math.PI),
    new ConstantDefinition("e",  Math.E),
    new ConstantDefinition("max",  double.MaxValue),
    new ConstantDefinition("min",  double.MinValue),
};

while (true)
{
    try
    {
        var tokenizer = new Tokenizer();
        var analyzer = new AbstractSyntaxTreeAnalyzer();
        var evaluator = new Evaluator(functions, constants);
        var input = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine($"Input: {input}\n");
        Console.WriteLine("Tokens:");

        var tokens = tokenizer.Tokenize(input);
        Console.WriteLine(string.Join('\n', tokens));
        Console.WriteLine();
        Console.WriteLine("AbstractSyntaxTreeAnalyzer:");

        var tree = analyzer.Analyze(tokens);

        Console.WriteLine(tree);
        Console.WriteLine();

        Console.WriteLine($"Output: {Math.Round(tree.Evaluate(evaluator), 5)}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
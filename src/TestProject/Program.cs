using MathExpressionParser;

while (true)
{
    try
    {
        var tokenizer = new Tokenizer();
        var analyzer = new AbstractSyntaxTreeAnalyzer();
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

        Console.WriteLine($"Output: {tree.Evaluate()}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
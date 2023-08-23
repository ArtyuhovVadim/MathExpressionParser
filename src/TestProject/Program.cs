using MathExpressionParser;

var parser = MathParserBuilder.BuildDefaultParser();

while (true)
{
    try
    {
        var input = Console.ReadLine()!;
        Console.Clear();
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Output: {parser.Parse(input)}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}
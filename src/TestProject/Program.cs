using MathExpressionParser;

var _tokenizer = new Tokenizer();
var _analyzer = new AbstractSyntaxTreeAnalyzer();
var _calculator = new TreeCalculator();

var bigInput = "(((2 + 2) * 2) / 4 + 0.5) * 2 ^ 2 - 10";
var smallInput = "(2 + 2) * 2";

try
{
    var input = smallInput;

    Console.WriteLine($"Input: {input}\n");

    Console.WriteLine("Tokens:");
    var tokens = _tokenizer.Tokenize(input).Where(x => x.Type != TokenType.Space);
    foreach (var token in tokens)
    {
        Console.WriteLine(token);
    }
    Console.WriteLine();
    Console.WriteLine("AbstractSyntaxTreeAnalyzer is not implemented.\n");
    Console.WriteLine("TreeCalculator is not implemented.\n");
}
catch (Exception e)
{
    Console.WriteLine(e);
}

Console.ReadKey();

using System.Linq;

namespace MathExpressionParser;

public class MathParser
{
    private readonly Tokenizer _tokenizer = new();
    private readonly AbstractSyntaxTreeAnalyzer _analyzer = new();
    private readonly TreeCalculator _calculator = new();

    public double Parse(string input)
    {
        var tokens = _tokenizer.Tokenize(input).Where(x => x.Type != TokenType.Space);
        var tree = _analyzer.Analyze(tokens);
        return _calculator.Calculate(tree);
    }
}
namespace MathExpressionParser;

public class MathParser
{
    private readonly Tokenizer _tokenizer = new();
    private readonly AbstractSyntaxTreeAnalyzer _analyzer = new();
    private readonly Evaluator _evaluator = new();

    public double Parse(string input)
    {
        var tokens = _tokenizer.Tokenize(input);
        var tree = _analyzer.Analyze(tokens);
        return tree.Evaluate(_evaluator);
    }
}
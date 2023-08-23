using System.Collections.Generic;

namespace MathExpressionParser;

public class MathParser
{
    private readonly Tokenizer _tokenizer = new();
    private readonly AbstractSyntaxTreeAnalyzer _analyzer = new();
    private readonly Evaluator _evaluator;

    public MathParser(IEnumerable<FunctionDefinition> functionDefinitions, IEnumerable<ConstantDefinition> constantDefinitions)
    {
        _evaluator = new Evaluator(functionDefinitions, constantDefinitions);
    }

    public double Parse(string input)
    {
        var tokens = _tokenizer.Tokenize(input);
        var tree = _analyzer.Analyze(tokens);
        return tree.Evaluate(_evaluator);
    }
}
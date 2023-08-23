namespace MathExpressionParser;

public class ConstantDefinition
{
    public ConstantDefinition(string name, double value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }

    public double Value { get; }
}
namespace MathExpressionParser;

public enum TokenType
{
    Number,
    Constant,
    Plus,
    Minus,
    Divide,
    //TODO: ModDivide,
    Multiply,
    Degree,
    LeftBracket,
    RightBracket,
    //TODO: Function,
    End
}
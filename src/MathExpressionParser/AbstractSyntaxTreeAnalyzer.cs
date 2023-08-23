using System;
using System.Collections.Generic;
using System.Linq;
using MathExpressionParser.Exceptions;
using MathExpressionParser.ExpressionNodes;
using MathExpressionParser.ExpressionNodes.Base;

namespace MathExpressionParser;

public class AbstractSyntaxTreeAnalyzer
{
    private int _pos;
    private List<Token> _tokens = null!;

    public ExpressionTreeNode Analyze(IEnumerable<Token> tokens)
    {
        _pos = 0;
        _tokens = tokens.ToList();

        var expression = Expression();

        if (!IsCurrentTokenOneOf(TokenType.End))
            throw CreateAnalyzerException(GetCurrent(), TokenType.End);

        return expression;
    }

    private ExpressionTreeNode Expression() => PlusMinus();

    private ExpressionTreeNode PlusMinus()
    {
        var operandA = DivideMultiply();

        while (IsCurrentTokenOneOf(TokenType.Plus, TokenType.Minus))
        {
            var token = GetNextAndMove();
            var operandB = DivideMultiply();
            operandA = new BinaryOperatorTreeNode(token, operandA, operandB);
        }

        return operandA;
    }

    private ExpressionTreeNode DivideMultiply()
    {
        var operandA = ModDiv();

        while (IsCurrentTokenOneOf(TokenType.Divide, TokenType.Multiply))
        {
            var token = GetNextAndMove();
            var operandB = ModDiv();
            operandA = new BinaryOperatorTreeNode(token, operandA, operandB);
        }

        return operandA;
    }

    private ExpressionTreeNode ModDiv()
    {
        var operandA = Degree();

        while (IsCurrentTokenOneOf(TokenType.Mod, TokenType.Div))
        {
            var token = GetNextAndMove();
            var operandB = Degree();
            operandA = new BinaryOperatorTreeNode(token, operandA, operandB);
        }

        return operandA;
    }

    private ExpressionTreeNode Degree()
    {
        var operandA = Unary();

        if (IsCurrentTokenOneOf(TokenType.Degree))
        {
            var token = GetNextAndMove();
            var operandB = Degree();
            return new BinaryOperatorTreeNode(token, operandA, operandB);
        }

        return operandA;
    }

    private ExpressionTreeNode Unary()
    {
        if (IsCurrentTokenOneOf(TokenType.Minus, TokenType.Plus))
        {
            var token = GetNextAndMove();

            return new UnaryOperatorTreeNode(token, Number());
        }

        return Number();
    }

    private ExpressionTreeNode Number()
    {
        if (IsCurrentTokenOneOf(TokenType.Function))
        {
            var token = GetNextAndMove();
            var arguments = new List<ExpressionTreeNode>();

            while (true)
            {
                arguments.Add(Expression());

                if (IsCurrentTokenOneOf(TokenType.Comma))
                {
                    MoveNext();
                    continue;
                }

                if (IsCurrentTokenOneOf(TokenType.RightBracket))
                {
                    MoveNext();
                    break;
                }

                throw CreateAnalyzerException(GetCurrent(), TokenType.Comma, TokenType.RightBracket);
            }

            return new FunctionTreeNode(token, arguments);
        }

        if (IsCurrentTokenOneOf(TokenType.Constant))
        {
            return new ConstantTreeNode(GetNextAndMove());
        }

        if (IsCurrentTokenOneOf(TokenType.Number))
        {
            return new NumberTreeNode(GetNextAndMove());
        }

        if (IsCurrentTokenOneOf(TokenType.LeftBracket))
        {
            MoveNext();
            var expression = Expression();

            if (IsCurrentTokenOneOf(TokenType.RightBracket))
            {
                MoveNext();
                return expression;
            }

            throw CreateAnalyzerException(GetCurrent(), TokenType.RightBracket);
        }

        throw CreateAnalyzerException(GetCurrent(), TokenType.Number, TokenType.Constant, TokenType.LeftBracket, TokenType.Function);
    }

    private bool IsCurrentTokenOneOf(params TokenType[] type)
    {
        if (_pos >= _tokens.Count)
            throw new IndexOutOfRangeException();

        return type.Any(x => x == _tokens[_pos].Type);
    }

    private void MoveNext() => _pos++;

    private Token GetNextAndMove()
    {
        if (_pos >= _tokens.Count)
            throw new IndexOutOfRangeException();

        return _tokens[_pos++];
    }

    private Token? GetCurrent()
    {
        if (_pos >= _tokens.Count)
            return null;

        return _tokens[_pos];
    }

    private AnalyzerException CreateAnalyzerException(Token? unexpectedToken, params TokenType[] expectedTypes) =>
        throw new AnalyzerException(_pos, expectedTypes, unexpectedToken, $"Unexpected token type ({unexpectedToken?.Type}) at position {_pos}, expected types is ({string.Join(", ", expectedTypes)}).");
}
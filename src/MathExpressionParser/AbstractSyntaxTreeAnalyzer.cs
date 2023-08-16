using System;
using System.Collections.Generic;
using System.Linq;

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

        if (!IsCurrentTokenIs(TokenType.End))
            throw CreateAnalyzerException(GetCurrent(), TokenType.End);

        return expression;
    }

    private ExpressionTreeNode Expression() => PlusMinus();

    private ExpressionTreeNode PlusMinus()
    {
        var operandA = DivideMultiply();

        while (IsCurrentTokenIs(TokenType.Plus) || IsCurrentTokenIs(TokenType.Minus))
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

        while (IsCurrentTokenIs(TokenType.Divide) || IsCurrentTokenIs(TokenType.Multiply))
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

        while (IsCurrentTokenIs(TokenType.Mod) || IsCurrentTokenIs(TokenType.Div))
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

        if (IsCurrentTokenIs(TokenType.Degree))
        {
            var token = GetNextAndMove();
            var operandB = Degree();
            return new BinaryOperatorTreeNode(token, operandA, operandB);
        }

        return operandA;
    }

    private ExpressionTreeNode Unary()
    {
        if (IsCurrentTokenIs(TokenType.Minus) || IsCurrentTokenIs(TokenType.Plus))
        {
            var token = GetNextAndMove();

            return new UnaryOperatorTreeNode(token, Number());
        }

        return Number();
    }

    private ExpressionTreeNode Number()
    {
        if (IsCurrentTokenIs(TokenType.Function))
        {
            var token = GetNextAndMove();
            var arguments = new List<ExpressionTreeNode>();

            while (true)
            {
                arguments.Add(Expression());

                if (IsCurrentTokenIs(TokenType.Comma))
                {
                    MoveNext();
                    continue;
                }

                if (IsCurrentTokenIs(TokenType.RightBracket))
                {
                    MoveNext();
                    break;
                }

                throw CreateAnalyzerException(GetCurrent(), TokenType.Comma, TokenType.RightBracket);
            }

            return new FunctionExpressionNode(token, arguments);
        }

        if (IsCurrentTokenIs(TokenType.Constant))
        {
            return new ConstantExpressionNode(GetNextAndMove());
        }

        if (IsCurrentTokenIs(TokenType.Number))
        {
            return new NumberTreeNode(GetNextAndMove());
        }

        if (IsCurrentTokenIs(TokenType.LeftBracket))
        {
            MoveNext();
            var expression = Expression();

            if (IsCurrentTokenIs(TokenType.RightBracket))
            {
                MoveNext();
                return expression;
            }

            throw CreateAnalyzerException(GetCurrent(), TokenType.RightBracket);
        }

        throw CreateAnalyzerException(GetCurrent(), TokenType.Number, TokenType.Constant, TokenType.LeftBracket, TokenType.Function);
    }

    private bool IsCurrentTokenIs(TokenType type)
    {
        if (_pos >= _tokens.Count)
            throw new IndexOutOfRangeException();

        return _tokens[_pos].Type == type;
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
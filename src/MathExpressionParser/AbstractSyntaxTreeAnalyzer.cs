using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExpressionParser;

public class AbstractSyntaxTreeAnalyzer
{
    /// <summary>
    /// Не участвует в построения АСД, нужно только для информировании об упущенном операторе
    /// </summary>
    private bool _isBinaryOperatorRequested;
    private int _pos;
    private List<Token> _tokens = null!;

    public ExpressionTreeNode Analyze(IEnumerable<Token> tokens)
    {
        _pos = 0;
        _tokens = tokens.ToList();
        _isBinaryOperatorRequested = false;

        var expression = Expression();

        if (_isBinaryOperatorRequested)
            throw CreateAnalyzerException(GetCurrent(), TokenType.Plus, TokenType.Minus, TokenType.Multiply, TokenType.Divide, TokenType.Degree);

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

            _isBinaryOperatorRequested = false;

            operandA = new BinaryOperatorTreeNode(token, operandA, operandB);
        }

        return operandA;
    }

    private ExpressionTreeNode DivideMultiply()
    {
        var operandA = Degree();

        while (IsCurrentTokenIs(TokenType.Divide) || IsCurrentTokenIs(TokenType.Multiply))
        {
            var token = GetNextAndMove();
            var operandB = Degree();

            _isBinaryOperatorRequested = false;

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

            _isBinaryOperatorRequested = false;

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
        if (IsCurrentTokenIs(TokenType.Constant))
        {
            var token = GetNextAndMove();

            if (!IsCurrentTokenIs(TokenType.End) && !IsCurrentTokenIs(TokenType.RightBracket))
                _isBinaryOperatorRequested = true;

            return new ConstantExpressionNode(token);
        }

        if (IsCurrentTokenIs(TokenType.Number))
        {
            var token = GetNextAndMove();

            if (!IsCurrentTokenIs(TokenType.End) && !IsCurrentTokenIs(TokenType.RightBracket))
                _isBinaryOperatorRequested = true;

            return new NumberTreeNode(token);
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

        throw CreateAnalyzerException(GetCurrent(), TokenType.Number, TokenType.Constant, TokenType.LeftBracket);
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
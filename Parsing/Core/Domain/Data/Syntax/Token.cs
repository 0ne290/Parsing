using Parsing.Core.Domain.Enums;

namespace Parsing.Core.Domain.Data.Syntax;

public class Token
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Token(string value) => Value = value;

    public string Value
    {
        get => _value;
        private init
        {
            switch (value)
            {
                case "=":
                    Type = TokenType.Operator;
                    Priority = 1;
                    _value = value;
                    break;
                case "+":
                    Type = TokenType.Operator;
                    Priority = 2;
                    _value = value;
                    break;
                case "*":
                    Type = TokenType.Operator;
                    Priority = 3;
                    _value = value;
                    break;
                case "(":
                    Type = TokenType.OpeningParenthesis;
                    _value = value;
                    break;
                case ")":
                    Type = TokenType.ClosingParenthesis;
                    _value = value;
                    break;
                default:
                    Type = TokenType.Operand;
                    _value = value;
                    break;
            }
        }
    }
    
    public TokenType Type { get; private set; }
    
    public int Priority { get; private set; }

    private readonly string _value;
}
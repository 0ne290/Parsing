using System.Globalization;
using Parsing.Core.Domain.Enums;

namespace Parsing.Core.Domain.Data.Syntax;

public class Name
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Name(string value) => Value = value;

    public NameType GetNameType(string oper)
    {
        if (int.TryParse(oper, out _))
            return NameType.IntConst;
        if (double.TryParse(oper, CultureInfo.InvariantCulture, out _))
            return NameType.FloatConst;
        
        return oper switch
        {
            "*" => NameType.Multiplication,
            "+" => NameType.Addition,
            _ => NameType.Variable
        };
    }
    
    public string Value
    {
        get => _value;
        private init
        {
            Type = GetNameType(value);

            _value = value;
        }
    }

    public NameType Type { get; private set; }
    
    public static Name Empty { get; } = new Name(string.Empty);
    
    private readonly string _value;
}
using System.Globalization;
using Parsing.Core.Domain.Enums;

namespace Parsing.Core.Domain.Data.Syntax;

public class Name
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Name(string value) => Value = value;
    
    public string Value
    {
        get => _value;
        private init
        {
            if (int.TryParse(value, out _))
                Type = NameType.IntConst;
            else if (double.TryParse(value, CultureInfo.InvariantCulture, out _))
                Type = NameType.FloatConst;
            else
                Type = NameType.Variable;

            _value = value;
        }
    }

    public NameType Type { get; private set; }
    
    private readonly string _value;
}
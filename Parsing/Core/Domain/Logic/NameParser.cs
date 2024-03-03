using Parsing.Core.Domain.Data.Syntax;
using Parsing.Core.Domain.Enums;
using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public class NameParser : INameParser
{
    public IEnumerable<Name> Parse(IEnumerable<Token> tokens) => tokens.Where(t => t.Type == TokenType.Operand)
        .Select(t => t.Value).Distinct().Select(o => new Name(o));
}
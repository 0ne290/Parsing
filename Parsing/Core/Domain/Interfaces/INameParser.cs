using Parsing.Core.Domain.Data.Syntax;

namespace Parsing.Core.Domain.Interfaces;

public interface INameParser
{
    IEnumerable<Name> Parse(IEnumerable<Token> tokens);
}
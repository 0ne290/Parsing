using Parsing.Core.Domain.Data.Syntax;

namespace Parsing.Core.Domain.Interfaces;

public interface ILogger
{
    void LogNameTable(IEnumerable<Name> nameTable);
}
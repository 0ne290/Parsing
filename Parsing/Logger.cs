using Parsing.Core.Domain.Data.Syntax;
using Parsing.Core.Domain.Interfaces;

namespace Parsing;

public class Logger : ILogger
{
    public void LogNameTable(IEnumerable<Name> nameTable)
    {
        Console.WriteLine("Name table:\n");
        foreach (var name in nameTable)
            Console.WriteLine($"{name.Value, 20} {name.Type, 20}");
    }
}
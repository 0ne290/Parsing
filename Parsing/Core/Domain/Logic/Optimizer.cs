using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public class Optimizer : IOptimizer
{
    public string Optimize(string code)
    {
        var regex = new Regex("LOAD [^;]+;STORE [^;]+;");
        return code.Replace(";", ";\n");
    }
}

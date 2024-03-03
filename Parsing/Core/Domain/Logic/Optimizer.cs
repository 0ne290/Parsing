using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public class Optimizer : IOptimizer
{
    public string Optimize(string code)
    {
        return code.Replace(";", ";\n");
    }
}
using System.Text;
using System.Text.RegularExpressions;
using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public partial class Optimizer : IOptimizer
{
    public string Optimize(string code)
    {
        
        return code.Replace(";", ";\n");
    }
    
    private string ApplyRuleThree(string code)
    {
        var regex = Regex3();

        var mathes = regex.Matches(code);

        foreach (Match match in mathes)
        {
            var beginIndex = match.Value.IndexOf("STORE ", StringComparison.Ordinal) + "STORE ".Length;
            var endIndex = match.Value.IndexOf(";", StringComparison.Ordinal);
            var operand = match.Value[beginIndex..endIndex];
            beginIndex = match.Value.IndexOf("LOAD ", StringComparison.Ordinal) + "LOAD ".Length;

            if (operand != match.Value[beginIndex..^1])
                continue;
            
            int beginbeginIndex;
            while ((beginbeginIndex = code.IndexOf(match.Value, StringComparison.Ordinal)) != -1)
            {
                beginIndex = beginbeginIndex + match.Value.Length;
                if (code.IndexOf($"STORE {operand}", beginIndex, StringComparison.Ordinal) > code.IndexOf(operand, beginIndex, StringComparison.Ordinal))
                    continue;
                
                var stringBuilder = new StringBuilder(code);
                stringBuilder.Remove(beginbeginIndex, match.Value.Length);

                code = stringBuilder.ToString();
            }
        }

        return code;
    }

    private string ApplyRuleFour(string code)
    {
        var regex = Regex4();

        var mathes = regex.Matches(code);

        foreach (Match match in mathes)
        {
            int beginbeginIndex;
            while ((beginbeginIndex = code.IndexOf(match.Value, StringComparison.Ordinal)) != -1)
            {
                var beginIndex = beginbeginIndex + match.Value.Length;
                
                if (code[beginIndex..(beginIndex + 4)] != "LOAD")
                    continue;
                
                var j = match.Value.IndexOf("STORE ", StringComparison.Ordinal);
                var operandB = match.Value[(j + "STORE ".Length)..^1];
                var operandA = match.Value[
                    (match.Value.IndexOf("LOAD ", StringComparison.Ordinal) + "LOAD ".Length)..
                    (j - 1)];
                var endIndex = code.IndexOf($"STORE {operandB};", beginIndex, StringComparison.Ordinal);
                endIndex = endIndex == -1 ? code.Length : endIndex;
                var stringBuilder = new StringBuilder(code);
                stringBuilder.Replace(operandB, operandA, beginIndex, code[beginIndex..endIndex].Length);
                stringBuilder.Remove(beginbeginIndex, match.Value.Length);

                code = stringBuilder.ToString();
            }
        }

        return code;
    }
    
    [GeneratedRegex("STORE [^;]+;LOAD [^;]+;")]
    private static partial Regex Regex3();

    [GeneratedRegex("LOAD [^;]+;STORE [^;]+;")]
    private static partial Regex Regex4();
}

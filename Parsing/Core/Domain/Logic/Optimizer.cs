using System.Text;
using System.Text.RegularExpressions;
using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public partial class Optimizer : IOptimizer
{
    public string Optimize(string code)
    {
        code = ApplyRuleFour(code);
        code = ApplyRuleThree(code);
        code = ApplyRuleOne(code);
        code = ApplyRuleTwo(code);
        code = ApplyRuleFour(code);
        code = ApplyRuleThree(code);
        return code.Replace(";", ";\n");
    }
    
    private string ApplyRuleOne(string code)
    {
        var regex = Regex1();

        var mathes = regex.Matches(code);

        foreach (Match match in mathes)
        {
            var beginIndex = match.Value.IndexOf("LOAD ", StringComparison.Ordinal) + "LOAD ".Length;
            var endIndex = match.Value.IndexOf(";", StringComparison.Ordinal);
            var operandA = match.Value[beginIndex..endIndex];
            beginIndex = match.Value.IndexOf("ADD ", StringComparison.Ordinal) + "ADD ".Length;
            var operandB = match.Value[beginIndex..^1];

            if (operandA == operandB)
                continue;
            
            var beginbeginIndex = 0;
            while ((beginbeginIndex = code.IndexOf(match.Value, beginbeginIndex, StringComparison.Ordinal)) != -1)
            {
                beginIndex = beginbeginIndex + match.Value.Length;
                if (code.IndexOf($"ADD {operandB}", beginIndex, StringComparison.Ordinal) != -1)
                {
                    beginbeginIndex = beginIndex;
                    continue;
                }
                
                var stringBuilder = new StringBuilder(code);
                stringBuilder.Replace(match.Value, $"LOAD {operandB};ADD {operandA};", beginbeginIndex, match.Value.Length);

                code = stringBuilder.ToString();
            }
        }

        return code;
    }
    
    private string ApplyRuleTwo(string code)
    {
        var regex = Regex2();

        var mathes = regex.Matches(code);

        foreach (Match match in mathes)
        {
            var beginIndex = match.Value.IndexOf("LOAD ", StringComparison.Ordinal) + "LOAD ".Length;
            var endIndex = match.Value.IndexOf(";", StringComparison.Ordinal);
            var operandA = match.Value[beginIndex..endIndex];
            beginIndex = match.Value.IndexOf("MPY ", StringComparison.Ordinal) + "MPY ".Length;
            var operandB = match.Value[beginIndex..^1];

            if (operandA == operandB)
                continue;
            
            var beginbeginIndex = 0;
            while ((beginbeginIndex = code.IndexOf(match.Value, beginbeginIndex, StringComparison.Ordinal)) != -1)
            {
                beginIndex = beginbeginIndex + match.Value.Length;
                if (code.IndexOf($"MPY {operandB}", beginIndex, StringComparison.Ordinal) != -1)
                {
                    beginbeginIndex = beginIndex;
                    continue;
                }
                
                var stringBuilder = new StringBuilder(code);
                stringBuilder.Replace(match.Value, $"LOAD {operandB};MPY {operandA};", beginbeginIndex, match.Value.Length);

                code = stringBuilder.ToString();
            }
        }

        return code;
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
            
            var beginbeginIndex = 0;
            while ((beginbeginIndex = code.IndexOf(match.Value, beginbeginIndex, StringComparison.Ordinal)) != -1)
            {
                beginIndex = beginbeginIndex + match.Value.Length;
                if (code.IndexOf($"STORE {operand}", beginIndex, StringComparison.Ordinal) > code.IndexOf(operand, beginIndex, StringComparison.Ordinal))
                {
                    beginbeginIndex = beginIndex;
                    continue;
                }
                
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
            var j = match.Value.IndexOf("STORE ", StringComparison.Ordinal);
            var operandB = match.Value[(j + "STORE ".Length)..^1];
            var operandA = match.Value[
                (match.Value.IndexOf("LOAD ", StringComparison.Ordinal) + "LOAD ".Length)..
                (j - 1)];

            if (operandA == operandB)
                continue;
            
            var beginbeginIndex = 0;
            while ((beginbeginIndex = code.IndexOf(match.Value, beginbeginIndex, StringComparison.Ordinal)) != -1)
            {
                var beginIndex = beginbeginIndex + match.Value.Length;
                
                if (code[beginIndex..(beginIndex + 4)] != "LOAD")
                {
                    beginbeginIndex = beginIndex;
                    continue;
                }
                
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
    
    [GeneratedRegex("LOAD [^;]+;ADD [^;]+;")]
    private static partial Regex Regex1();

    [GeneratedRegex("LOAD [^;]+;MPY [^;]+;")]
    private static partial Regex Regex2();
    
    [GeneratedRegex("STORE [^;]+;LOAD [^;]+;")]
    private static partial Regex Regex3();

    [GeneratedRegex("LOAD [^;]+;STORE [^;]+;")]
    private static partial Regex Regex4();
}

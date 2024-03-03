using Parsing.Core.Domain.Data.BinarySyntaxTree;
using Parsing.Core.Domain.Data.Syntax;
using Parsing.Core.Domain.Enums;
using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public class Translator
{
    public Translator(IStateMachine stateMachine, INameParser nameParser, ISyntaxTreeBuilder syntaxTreeBuilder, IOptimizer optimizer, ILogger logger)
    {
        _stateMachine = stateMachine;
        _nameParser = nameParser;
        _syntaxTreeBuilder = syntaxTreeBuilder;
        _optimizer = optimizer;
        _logger = logger;
    }

    public void Translate(string inputString)
    {
        inputString = inputString.Replace(" ", "");
        inputString = inputString.Replace("\n", "");

        string leftOperand;
        try
        {
            leftOperand = inputString[..inputString.IndexOf('=')];
        }
        catch (Exception)
        {
            throw new Exception("Assignment operator not found");
        }
        
        var rightOperand = inputString[(inputString.IndexOf('=') + 1)..] + '\0';

        if (Name.GetNameType(leftOperand) != NameType.Variable)
            throw new Exception("The left operand must be a variable");
        
        var tokens = _stateMachine.Parse(rightOperand.ToCharArray());

        _logger.LogNameTable(_nameParser.Parse(tokens)); 

        var rootOfSyntaxTree = _syntaxTreeBuilder.BuildSyntaxTree(tokens);
        
        TreeNode.GenerateCode();

        var unoptimizedCode = $"LOAD {rootOfSyntaxTree.Code};\nSTORE {leftOperand};".Replace(";;", ";");

        var optimizedCode = _optimizer.Optimize(unoptimizedCode.Replace("\n", ""));
        
        Console.WriteLine("\n\nUnoptimized Code:\n");
        Console.WriteLine(unoptimizedCode);
        
        Console.WriteLine("\n\nOptimized Code:\n");
        Console.WriteLine(optimizedCode);
    }
    
    private readonly IStateMachine _stateMachine;

    private readonly INameParser _nameParser;

    private readonly ISyntaxTreeBuilder _syntaxTreeBuilder;
    
    private readonly IOptimizer _optimizer;

    private readonly ILogger _logger;
}
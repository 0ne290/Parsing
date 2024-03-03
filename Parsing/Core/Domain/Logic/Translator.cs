using Parsing.Core.Domain.Data.BinarySyntaxTree;
using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public class Translator
{
    public Translator(IStateMachine stateMachine, INameParser nameParser, ISyntaxTreeBuilder syntaxTreeBuilder, ILogger logger)
    {
        _stateMachine = stateMachine;
        _nameParser = nameParser;
        _syntaxTreeBuilder = syntaxTreeBuilder;
        _logger = logger;
    }

    public void Translate(char[] inputString)
    {
        var tokens = _stateMachine.Parse(inputString);

        _logger.LogNameTable(_nameParser.Parse(tokens)); 

        var rootOfSyntaxTree = _syntaxTreeBuilder.BuildSyntaxTree(tokens);
        
        TreeNode.GenerateCode();
        
        Console.WriteLine(rootOfSyntaxTree.Code);
    }
    
    private readonly IStateMachine _stateMachine;

    private readonly INameParser _nameParser;

    private readonly ISyntaxTreeBuilder _syntaxTreeBuilder;

    private readonly ILogger _logger;
}
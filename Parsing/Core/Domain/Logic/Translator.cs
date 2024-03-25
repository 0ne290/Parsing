using Parsing.Core.Domain.Data.BinarySyntaxTree;
using Parsing.Core.Domain.Data.StateMachine;
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

    public static Translator Create(State state)
    {
        var pushdownAutomaton = new PushdownAutomaton(state);
        var nameParser = new NameParser();
        var marshallingYardAlgorithm = new MarshallingYardAlgorithm();
        var optimizer = new Optimizer();
        var logger = new Logger();

        return new Translator(pushdownAutomaton, nameParser, marshallingYardAlgorithm, optimizer, logger);
    }

    public void Translate(string inputString)
    {
        inputString += '\0';
        
        var tokens = _stateMachine.Parse(inputString.ToCharArray());

        _logger.LogNameTable(_nameParser.Parse(tokens)); 

        var rootOfSyntaxTree = _syntaxTreeBuilder.BuildSyntaxTree(tokens);
        
        TreeNode.GenerateCode();

        var unoptimizedCode = rootOfSyntaxTree.Code.Replace(";;", ";");

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

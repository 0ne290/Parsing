namespace Parsing;

public class PushdownAutomaton
{
    public PushdownAutomaton(State initialState) => _currentState = initialState;
    
    public TreeNode BuildSyntaxTree(char[] inputString)
    {
        var treeNode = new TreeNode();
        
        _stack.Push(default);
        
        while (!_currentState.IsFinal)
        {
            var transition = _currentState.ExecuteTransition(inputString[_inputStringIndex]);
            transition.StackAction(_stack);
            transition.LexemeAction(_lexeme);
            treeNode = transition.TreeAction(treeNode);
            _currentState = transition.State;
            _inputStringIndex++;
        }

        return treeNode.GetRoot();
    }
    
    private State _currentState;

    private readonly Stack<char> _stack = new();
    
    private readonly List<char> _lexeme = new();

    private int _inputStringIndex;
}
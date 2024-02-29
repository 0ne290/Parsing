namespace Parsing;

public class PushdownAutomaton
{
    public PushdownAutomaton(State initialState) => _currentState = initialState;
    
    public TreeNode BuildSyntaxTree(char[] _inputString)
    {
        
    }
    
    private State _currentState;

    private Stack<char> _stack = new();
}
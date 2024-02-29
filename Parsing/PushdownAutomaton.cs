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
            var resultOfTransitionFunction = _currentState.Transition(inputString[_inputStringIndex], _stack.Pop());
            _currentState = resultOfTransitionFunction.Item1;
            treeNode = _currentState.ExecuteAction(treeNode);
            _stack.Push(resultOfTransitionFunction.Item2);
            _inputStringIndex++;
        }

        return treeNode.GetRoot();
    }
    
    private State _currentState;

    private readonly Stack<char> _stack = new();

    private int _inputStringIndex;
}
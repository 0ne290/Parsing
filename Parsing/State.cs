namespace Parsing;

public class State
{
    public State(bool isFinal) => IsFinal = isFinal;

    public State(Action<TreeNode> action) => _action = action;
    
    public State(bool isFinal, Action<TreeNode> action)
    {
        IsFinal = isFinal;
        _action = action;
    }
    
    public void ExecuteAction(TreeNode node) => _action(node);

    public State Transition(char charFromInputString, char charFromStack) =>
        _adjacentStates[$"{charFromInputString}{charFromStack}"];

    public void AddState(char charFromInputString, char charFromStack, State state) =>
        _adjacentStates.Add($"{charFromInputString}{charFromStack}", state);
    
    public bool IsFinal { get; }

    private readonly Action<TreeNode> _action = (_) => { };

    private readonly Dictionary<string, State> _adjacentStates = new();
}
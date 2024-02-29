namespace Parsing;

public class State
{
    public State() { }
    
    public State(bool isFinal) => IsFinal = isFinal;

    public State(Func<TreeNode, TreeNode> action) => _action = action;
    
    public State(bool isFinal, Func<TreeNode, TreeNode> action)
    {
        IsFinal = isFinal;
        _action = action;
    }
    
    public TreeNode ExecuteAction(TreeNode node) => _action(node);

    public Tuple<State, char> Transition(char charFromInputString, char charFromStack) =>
        _adjacentStates[$"{charFromInputString}{charFromStack}"];

    public void AddState(char charFromInputString, char charFromStack, Tuple<State, char> state) =>
        _adjacentStates.Add($"{charFromInputString}{charFromStack}", state);
    
    public bool IsFinal { get; }

    private readonly Func<TreeNode, TreeNode> _action = node => node;

    private readonly Dictionary<string, Tuple<State, char>> _adjacentStates = new();
}
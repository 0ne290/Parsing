namespace Parsing;

public class Transition
{
    public Transition(State state, Func<TreeNode, TreeNode>? treeAction = null, Action<Stack<char>>? stackAction = null, Action<List<char>>? lexemeAction = null)
    {
        State = state;
        TreeAction = treeAction ?? (node => node);
        StackAction = stackAction ?? (_ => { });
        LexemeAction = lexemeAction ?? (_ => { });
    }
    
    public State State { get; }
    
    public Func<TreeNode, TreeNode> TreeAction { get; }
    
    public Action<Stack<char>> StackAction { get; }
    
    public Action<List<char>> LexemeAction { get; }
}
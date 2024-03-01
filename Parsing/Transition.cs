namespace Parsing;

public class Transition
{
    public Transition(State state, Func<List<char>, TreeNode, TreeNode>? treeAction = null, Action<Stack<char>>? stackAction = null, Action<List<char>, char>? lexemeAction = null)
    {
        State = state;
        TreeAction = treeAction ?? ((_, node) => node);
        StackAction = stackAction ?? (_ => { });
        LexemeAction = lexemeAction ?? ((_, _) => { });
    }
    
    public State State { get; }
    
    public Func<List<char>, TreeNode, TreeNode> TreeAction { get; }
    
    public Action<Stack<char>> StackAction { get; }
    
    public Action<List<char>, char> LexemeAction { get; }

    public static Func<List<char>, TreeNode, TreeNode> TreeActionOpeningParenthesis { get; } = (_, node) =>
    {
        var newNode = new TreeNode(node);
        node.LeftChild = newNode;
        return newNode;
    };
    
    public static Func<List<char>, TreeNode, TreeNode> TreeActionOperator { get; } = (lexeme, node) =>
    {
        
        
        node.Oper = new string(lexeme.ToArray());// Это для операнда. Получить массив всех символов без последнего (последний - знак оператора + или *). Добавить аналогичный код для оператора
    };

    public static Func<List<char>, TreeNode, TreeNode> TreeActionClosingParenthesis { get; } = (_, node) => node.Parent;

    public static Action<Stack<char>> StackActionPush { get; } = stack => stack.Push('+');

    public static Action<Stack<char>> StackActionPop { get; } = stack =>
    {
        var character = stack.Pop();

        if (character != '+')
            throw new Exception("Expected '+' character on stack");
    };
    
    public static Action<List<char>, char> LexemeActionAddChar { get; } = (lexeme, character) => lexeme.Add(character);
}
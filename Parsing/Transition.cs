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
    
    /*
        ПРЕДУПРЕЖДЕНИЕ! ДАННОЕ ДЕЙСТВИЕ ОЧИЩАЕТ ЛЕКСЕМУ!
        Я понимаю, что очищение лексемы по-хорошему должно выполняться в действиях с лексемой (методы с префиксом,
        LexemeAction), а не в действиях с деревом (методы с префиксом, TreeAction), но я всё-таки принял решение
        ввести этот небольшой костыль, т. к. иначе потребовалось бы переписать части компонентов системы
    */
    public static Func<List<char>, TreeNode, TreeNode> TreeActionOperatorAfterToken { get; } = (lexeme, node) =>
    {
        var operand = new char[lexeme.Count - 1];
        lexeme.CopyTo(0, operand, 0, operand.Length);
        node.Oper = new string(operand);
        
        
        
        var operatorNode = node.Parent;

        if (operatorNode == node)
        {
            Console.WriteLine("ghh34");
            operatorNode = new TreeNode
            {
                LeftChild = node
            };
        }
        
        operatorNode.Oper = lexeme[^1].ToString();
        var newNode = new TreeNode(operatorNode);
        operatorNode.RightChild = newNode;
        
        lexeme.Clear();// Вот это гадство

        return newNode;
    };
    
    public static Func<List<char>, TreeNode, TreeNode> TreeActionOperatorAfterClosingParenthesis { get; } = (lexeme, node) =>
    {
        node.Oper = lexeme[^1].ToString();
        var newNode = new TreeNode(node);
        node.RightChild = newNode;
        
        return newNode;
    };
    
    public static Func<List<char>, TreeNode, TreeNode> TreeActionEnd { get; } = (lexeme, node) =>
    {
        node.Oper = new string(lexeme.ToArray());

        return node;
    };

    public static Func<List<char>, TreeNode, TreeNode> TreeActionNestedClosingParenthesis { get; } = (_, node) => node.Parent;

    public static Func<List<char>, TreeNode, TreeNode> TreeActionClosingParenthesisAfterToken { get; } = (lexeme, node) =>
    {
        node.Oper = new string(lexeme.ToArray());
        
        lexeme.Clear();

        return node.Parent.Parent;
    };

    public static Action<Stack<char>> StackActionPush { get; } = stack => stack.Push('+');

    public static Action<Stack<char>> StackActionPop { get; } = stack =>
    {
        var character = stack.Pop();

        if (character != '+')
            throw new Exception("Expected '+' character on stack");
    };
    
    public static Action<Stack<char>> StackActionEmpty { get; } = stack =>
    {
        if (stack.Count > 0)
            throw new Exception("The stack was expected to be empty");
    };
    
    public static Action<List<char>, char> LexemeActionAddChar { get; } = (lexeme, character) => lexeme.Add(character);
}
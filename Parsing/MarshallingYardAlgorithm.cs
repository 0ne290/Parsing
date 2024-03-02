namespace Parsing;

public class MarshallingYardAlgorithm
{
    public MarshallingYardAlgorithm(IEnumerable<Token> tokens)
    {
        _tokens = tokens;
    }
    
    public TreeNode BuildSyntaxTree()
    {
        foreach (var token in _tokens)
        {
            switch (token.Type)
            {
                case TokenType.Operand:
                    _nodeStack.Push(new TreeNode(token.Value));
                    break;
                case TokenType.Operator:
                {
                    while (_operatorStack.TryPeek(out var t) && t.Type != TokenType.OpeningParenthesis && t.Priority >= token.Priority)
                    {
                        _nodeStack.Push(new TreeNode(_operatorStack.Pop().Value, _nodeStack.Pop(), _nodeStack.Pop()));
                    }
                    _operatorStack.Push(token);
                    break;
                }
                case TokenType.OpeningParenthesis:
                    _operatorStack.Push(token);
                    break;
                case TokenType.ClosingParenthesis:
                {
                    while (_operatorStack.Peek().Type != TokenType.OpeningParenthesis)
                    {
                        _nodeStack.Push(new TreeNode(_operatorStack.Pop().Value, _nodeStack.Pop(), _nodeStack.Pop()));
                    }
                    _operatorStack.Pop();
                    break;
                }
            }
        }
        
        while (_operatorStack.Count > 0)
        {
            _nodeStack.Push(new TreeNode(_operatorStack.Pop().Value, _nodeStack.Pop(), _nodeStack.Pop()));
        }

        return _nodeStack.Pop();
    }

    private readonly IEnumerable<Token> _tokens;
    
    private readonly Stack<Token> _operatorStack = new();
    
    private readonly Stack<TreeNode> _nodeStack = new();
}
using Parsing.Core.Domain.Data.BinarySyntaxTree;
using Parsing.Core.Domain.Data.Syntax;
using Parsing.Core.Domain.Enums;
using Parsing.Core.Domain.Interfaces;

namespace Parsing.Core.Domain.Logic;

public class MarshallingYardAlgorithm : ISyntaxTreeBuilder
{
    public TreeNode BuildSyntaxTree(IEnumerable<Token> tokens)
    {
        Stack<Token> operatorStack = new();
        Stack<TreeNode> nodeStack = new();
        
        foreach (var token in tokens)
        {
            switch (token.Type)
            {
                case TokenType.Operand:
                    nodeStack.Push(new TreeNode(new Name(token.Value)));
                    
                    break;
                case TokenType.Operator:
                {
                    while (operatorStack.TryPeek(out var t) && t.Type != TokenType.OpeningParenthesis && t.Priority >= token.Priority)
                        nodeStack.Push(new TreeNode(new Name(operatorStack.Pop().Value), nodeStack.Pop(), nodeStack.Pop()));
                    
                    operatorStack.Push(token);
                    
                    break;
                }
                case TokenType.OpeningParenthesis:
                    operatorStack.Push(token);
                    
                    break;
                case TokenType.ClosingParenthesis:
                {
                    while (operatorStack.Peek().Type != TokenType.OpeningParenthesis)
                        nodeStack.Push(new TreeNode(new Name(operatorStack.Pop().Value), nodeStack.Pop(), nodeStack.Pop()));
                    
                    operatorStack.Pop();
                    
                    break;
                }
                default:
                    throw new Exception("Unable to process token of unknown type");
            }
        }
        
        while (operatorStack.Count > 0)
            nodeStack.Push(new TreeNode(new Name(operatorStack.Pop().Value), nodeStack.Pop(), nodeStack.Pop()));

        return nodeStack.Pop();
    }
}
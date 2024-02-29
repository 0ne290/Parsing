using System.Runtime.CompilerServices;

namespace Parsing;

public class TreeNode
{
    public TreeNode()
    {
        Oper = string.Empty;
        Parent = this;
        LeftChild = this;
        RightChild = this;
    }

    public bool IsLeaf() => SubtreeIsEmpty(LeftChild) && SubtreeIsEmpty(RightChild);

    public bool IsRoot() => SubtreeIsEmpty(Parent);

    public bool SubtreeIsEmpty(TreeNode node) => node == this;

    public string Oper { get; set; }

    public TreeNode Parent { get; set; }
    
    public TreeNode LeftChild { get; set; }
    
    public TreeNode RightChild { get; set; }
}
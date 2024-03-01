using System.Runtime.CompilerServices;

namespace Parsing;

public class TreeNode
{
    public TreeNode(TreeNode? parent = null)
    {
        Oper = string.Empty;
        Parent = parent ?? this;
        LeftChild = this;
        RightChild = this;
    }

    public TreeNode GetRoot()
    {
        var root = this;

        while (!root.IsRoot())
            root = root.Parent;

        return root;
    }

    public bool IsLeaf() => SubtreeIsEmpty(LeftChild) && SubtreeIsEmpty(RightChild);

    public bool IsRoot() => SubtreeIsEmpty(Parent);

    public bool SubtreeIsEmpty(TreeNode node) => node == this;

    public string Oper { get; set; }

    public TreeNode Parent { get; set; }
    
    public TreeNode LeftChild { get; set; }
    
    public TreeNode RightChild { get; set; }
}
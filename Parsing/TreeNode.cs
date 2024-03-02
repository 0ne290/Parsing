namespace Parsing;

public class TreeNode
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TreeNode(string oper = "", TreeNode? leftChild = null, TreeNode? rightChild = null)
    {
        Oper = oper;
        Parent = this;
        LeftChild = leftChild ?? this;
        RightChild = rightChild ?? this;
    }

    //public TreeNode GetRoot()
    //{
    //    var root = this;
//
    //    while (!root.IsRoot())
    //        root = root.Parent;
//
    //    return root;
    //}

    //public bool IsLeaf() => SubtreeIsEmpty(LeftChild) && SubtreeIsEmpty(RightChild);

    //public bool IsRoot() => SubtreeIsEmpty(Parent);

    //public bool SubtreeIsEmpty(TreeNode node) => node == this;

    public string Oper { get; set; }

    public TreeNode Parent { get; set; }

    public TreeNode LeftChild
    {
        get => _leftChild;
        set
        {
            _leftChild = value;
            _leftChild.Parent = this;
        }
    }
    
    public TreeNode RightChild
    {
        get => _rightChild;
        set
        {
            _rightChild = value;
            _rightChild.Parent = this;
        }
    }

    private TreeNode _leftChild;
    
    private TreeNode _rightChild;
}
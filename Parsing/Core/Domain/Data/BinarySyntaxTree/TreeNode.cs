using Parsing.Core.Domain.Data.Syntax;
using Parsing.Core.Domain.Enums;

namespace Parsing.Core.Domain.Data.BinarySyntaxTree;

public class TreeNode
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TreeNode(Name? oper = null, TreeNode? leftChild = null, TreeNode? rightChild = null)
    {
        Oper = oper ?? Name.Empty;
        Parent = this;
        LeftChild = leftChild ?? this;
        RightChild = rightChild ?? this;
        
        Nodes.Add(this);
    }

    public static void GenerateCode()
    {
        GenerateLevelOfAllNodes();

        var nodesByLevel = Nodes.OrderBy(n => n.Level);

        foreach (var node in nodesByLevel)
        {
            node.Code = node.Oper.Type switch
            {
                NameType.Variable => node.Oper.Value,
                NameType.Assignment => $"LOAD {node.LeftChild.Code};\nSTORE {node.RightChild.Code};",
                NameType.Addition =>
                    $"{node.RightChild.Code};\nSTORE $l{node.Level};\nLOAD {node.LeftChild.Code};\nADD $l{node.Level};",
                NameType.Multiplication =>
                    $"{node.RightChild.Code};\nSTORE $l{node.Level};\nLOAD {node.LeftChild.Code};\nMPY $l{node.Level};",
                _ => "=" + node.Oper.Value
            };
        }
    }

    private static void GenerateLevelOfAllNodes()
    {
        var leaves = Nodes.Where(n => n.IsLeaf());

        foreach (var leaf in leaves)
        {
            var level = 0;
            var node = leaf;

            do
            {
                if (node.Level < level)
                    node.Level = level;

                node = node.Parent;
                level++;
            } while (!node.IsRoot());
            
            if (node.Level < level)
                node.Level = level;
        }
    }

    public bool IsLeaf() => SubtreeIsEmpty(LeftChild) && SubtreeIsEmpty(RightChild);
    
    public bool IsRoot() => SubtreeIsEmpty(Parent);

    public bool SubtreeIsEmpty(TreeNode node) => node == this;

    public Name Oper { get; set; }

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
    
    public string Code { get; set; }
    
    public int Level { get; set; }

    public static List<TreeNode> Nodes = new();

    private TreeNode _leftChild;
    
    private TreeNode _rightChild;
}
using Parsing.Core.Domain.Data.BinarySyntaxTree;
using Parsing.Core.Domain.Data.Syntax;

namespace Parsing.Core.Domain.Interfaces;

public interface ISyntaxTreeBuilder
{
    TreeNode BuildSyntaxTree(IEnumerable<Token> tokens);
}
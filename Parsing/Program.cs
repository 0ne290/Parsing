namespace Parsing;

internal static class Program
{
    private static void Main()
    {
        
    }

    private static void RootOfComposition()
    {
        var state1 = new State();
        state1.AddState('(', default, Tuple.Create(state1, '+'));
        
        var state2 = new State(node =>
        {
            var newNode = new TreeNode();
            node.LeftChild = newNode;
            return newNode;
        });
        state2.AddState('(', default, Tuple.Create(state1, default(char)));
        state1.AddState('(', default, Tuple.Create(state1, '+'));
        
        
        var state3 = new State();
        var state4 = new State();
        var state5 = new State();
        var state6 = new State();
        var state7 = new State();
        var state8 = new State();
        var state9 = new State();
        var state10 = new State();
        var state11 = new State(true);
    }
}
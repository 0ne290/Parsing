namespace Parsing;

internal static class Program
{
    private static void Main()
    {
        
    }

    private static void RootOfComposition()
    {
        var one = new State();
        one.AddState('(', new Transition(one, Transition.TreeActionOpeningParenthesis, Transition.StackActionPush));

        var five = new State();
        one.AddState('0', new Transition(five, lexemeAction: Transition.LexemeActionAddChar));
        
        var ten = new State();
        var transition = new Transition(ten, lexemeAction: Transition.LexemeActionAddChar);
        five.AddState('.', transition);
        
        ten.AddState('0', transition);
        ten.AddState('1', transition);
        ten.AddState('2', transition);
        ten.AddState('3', transition);
        ten.AddState('4', transition);
        ten.AddState('5', transition);
        ten.AddState('6', transition);
        ten.AddState('7', transition);
        ten.AddState('8', transition);
        ten.AddState('9', transition);
        
        ten.AddState('+', new Transition(one));
        
        
        
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
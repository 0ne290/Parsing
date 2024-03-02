namespace Parsing;

internal static class Program
{
    private static void Main()
    {
        var stateMachine = RootOfComposition();

        var tokens = stateMachine.Parse("(x+4)*(r46h+87)\0".ToCharArray());

        foreach (var token in tokens)
        {
            Console.WriteLine($"{token.Value} {token.Type} {token.Priority}");
        }
    }

    private static PushdownAutomaton RootOfComposition()
    {
        var one = new State("one");
        var nine = new State("nine");
        
        
        var finalState = new State("final", true);
        var finalTransition = new Transition(finalState, Transition.StackActionEmpty, Transition.LexemeActionOperandRecognized);
        var operandAndOperatorTransition =
                    new Transition(one, lexemeAction: Transition.LexemeActionOperandAndOperatorRecognized);
        var operandAndClosingParenthesisTransition =
            new Transition(nine, Transition.StackActionPop, Transition.LexemeActionOperandAndOperatorRecognized);
        
        
        one.AddState('(', new Transition(one, Transition.StackActionPush, Transition.LexemeActionOperatorRecognized));
        nine.AddState(')', new Transition(nine, Transition.StackActionPop, Transition.LexemeActionOperatorRecognized));
        var transition = new Transition(one, lexemeAction: Transition.LexemeActionOperatorRecognized);
        nine.AddState('+', transition);
        nine.AddState('*', transition);
        nine.AddState('\0', new Transition(finalState, Transition.StackActionEmpty));
        
        
        var five = new State("five");
        
        one.AddState('0', new Transition(five, lexemeAction: Transition.LexemeActionAddChar));
        
        
        var eigth = new State("eigth");
        
        transition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);
        
        eigth.AddState('0', transition);
        eigth.AddState('1', transition);
        eigth.AddState('2', transition);
        eigth.AddState('3', transition);
        eigth.AddState('4', transition);
        eigth.AddState('5', transition);
        eigth.AddState('6', transition);
        eigth.AddState('7', transition);
        eigth.AddState('8', transition);
        eigth.AddState('9', transition);
        
        eigth.AddState(')', operandAndClosingParenthesisTransition);
        
        eigth.AddState('+', operandAndOperatorTransition);
        eigth.AddState('*', operandAndOperatorTransition);
        
        eigth.AddState('\0', finalTransition);
        
        
        var seven = new State("seven");
        seven.AddState('1', transition);
        seven.AddState('2', transition);
        seven.AddState('3', transition);
        seven.AddState('4', transition);
        seven.AddState('5', transition);
        seven.AddState('6', transition);
        seven.AddState('7', transition);
        seven.AddState('8', transition);
        seven.AddState('9', transition);
        
        
        var six = new State("six");
        transition = new Transition(seven, lexemeAction: Transition.LexemeActionAddChar);
        six.AddState('+', transition);
        six.AddState('-', transition);
        
        
        var ten = new State("ten");
        transition = new Transition(ten, lexemeAction: Transition.LexemeActionAddChar);
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
        
        ten.AddState('E', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        
        ten.AddState(')', operandAndClosingParenthesisTransition);
        
        ten.AddState('+', operandAndOperatorTransition);
        ten.AddState('*', operandAndOperatorTransition);
        
        ten.AddState('\0', finalTransition);
        
        
        
        var four = new State("four");
        transition = new Transition(four, lexemeAction: Transition.LexemeActionAddChar);
        one.AddState('1', transition);
        one.AddState('2', transition);
        one.AddState('3', transition);
        one.AddState('4', transition);
        one.AddState('5', transition);
        one.AddState('6', transition);
        one.AddState('7', transition);
        one.AddState('8', transition);
        one.AddState('9', transition);
        
        four.AddState('0', transition);
        four.AddState('1', transition);
        four.AddState('2', transition);
        four.AddState('3', transition);
        four.AddState('4', transition);
        four.AddState('5', transition);
        four.AddState('6', transition);
        four.AddState('7', transition);
        four.AddState('8', transition);
        four.AddState('9', transition);
        
        four.AddState('.', new Transition(ten, lexemeAction: Transition.LexemeActionAddChar));
        
        four.AddState('E', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        
        four.AddState(')', operandAndClosingParenthesisTransition);
        
        four.AddState('+', operandAndOperatorTransition);
        four.AddState('*', operandAndOperatorTransition);
        
        four.AddState('\0', finalTransition);
        
        
        
        var three = new State("three");
        transition = new Transition(three, lexemeAction: Transition.LexemeActionAddChar);
        one.AddState('A', transition);
        one.AddState('B', transition);
        one.AddState('C', transition);
        one.AddState('D', transition);
        one.AddState('E', transition);
        one.AddState('F', transition);
        one.AddState('G', transition);
        one.AddState('H', transition);
        one.AddState('I', transition);
        one.AddState('J', transition);
        one.AddState('K', transition);
        one.AddState('L', transition);
        one.AddState('M', transition);
        one.AddState('N', transition);
        one.AddState('O', transition);
        one.AddState('P', transition);
        one.AddState('Q', transition);
        one.AddState('R', transition);
        one.AddState('S', transition);
        one.AddState('T', transition);
        one.AddState('U', transition);
        one.AddState('V', transition);
        one.AddState('W', transition);
        one.AddState('X', transition);
        one.AddState('Y', transition);
        one.AddState('Z', transition);
        
        one.AddState('a', transition);
        one.AddState('b', transition);
        one.AddState('c', transition);
        one.AddState('d', transition);
        one.AddState('e', transition);
        one.AddState('f', transition);
        one.AddState('g', transition);
        one.AddState('h', transition);
        one.AddState('i', transition);
        one.AddState('j', transition);
        one.AddState('k', transition);
        one.AddState('l', transition);
        one.AddState('m', transition);
        one.AddState('n', transition);
        one.AddState('o', transition);
        one.AddState('p', transition);
        one.AddState('q', transition);
        one.AddState('r', transition);
        one.AddState('s', transition);
        one.AddState('t', transition);
        one.AddState('u', transition);
        one.AddState('v', transition);
        one.AddState('w', transition);
        one.AddState('x', transition);
        one.AddState('y', transition);
        one.AddState('z', transition);
        
        three.AddState('A', transition);
        three.AddState('B', transition);
        three.AddState('C', transition);
        three.AddState('D', transition);
        three.AddState('E', transition);
        three.AddState('F', transition);
        three.AddState('G', transition);
        three.AddState('H', transition);
        three.AddState('I', transition);
        three.AddState('J', transition);
        three.AddState('K', transition);
        three.AddState('L', transition);
        three.AddState('M', transition);
        three.AddState('N', transition);
        three.AddState('O', transition);
        three.AddState('P', transition);
        three.AddState('Q', transition);
        three.AddState('R', transition);
        three.AddState('S', transition);
        three.AddState('T', transition);
        three.AddState('U', transition);
        three.AddState('V', transition);
        three.AddState('W', transition);
        three.AddState('X', transition);
        three.AddState('Y', transition);
        three.AddState('Z', transition);
        
        three.AddState('a', transition);
        three.AddState('b', transition);
        three.AddState('c', transition);
        three.AddState('d', transition);
        three.AddState('e', transition);
        three.AddState('f', transition);
        three.AddState('g', transition);
        three.AddState('h', transition);
        three.AddState('i', transition);
        three.AddState('j', transition);
        three.AddState('k', transition);
        three.AddState('l', transition);
        three.AddState('m', transition);
        three.AddState('n', transition);
        three.AddState('o', transition);
        three.AddState('p', transition);
        three.AddState('q', transition);
        three.AddState('r', transition);
        three.AddState('s', transition);
        three.AddState('t', transition);
        three.AddState('u', transition);
        three.AddState('v', transition);
        three.AddState('w', transition);
        three.AddState('x', transition);
        three.AddState('y', transition);
        three.AddState('z', transition);
        
        three.AddState('0', transition);
        three.AddState('1', transition);
        three.AddState('2', transition);
        three.AddState('3', transition);
        three.AddState('4', transition);
        three.AddState('5', transition);
        three.AddState('6', transition);
        three.AddState('7', transition);
        three.AddState('8', transition);
        three.AddState('9', transition);
        
        three.AddState(')', operandAndClosingParenthesisTransition);
        
        three.AddState('+', operandAndOperatorTransition);
        three.AddState('*', operandAndOperatorTransition);
        
        three.AddState('\0', finalTransition);


        return new PushdownAutomaton(one);
    }
}
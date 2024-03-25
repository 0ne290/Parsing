using Parsing.Core.Domain.Data.StateMachine;
using Parsing.Core.Domain.Logic;

namespace Parsing;

internal static class Program
{
    private static int Main()
    {
        try
        {
            var translator = RootOfComposition();

            var reader = new StreamReader("input.txt", System.Text.Encoding.UTF8);

            var inputString = reader.ReadToEnd();
        
            reader.Dispose();
        
            translator.Translate(inputString);

            Console.Write("Нажмите любую клавишу для завершения программы...");
            Console.ReadKey();

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nНажмите любую клавишу для завершения программы...");
            Console.ReadKey();
            
            return 1;
        }
    }

    private static Translator RootOfComposition()
    {
        var one = State.One();
        var two = State.Two();
        var nine = State.Nine();
        
        
        var finalState = State.Final();
        var finalTransition = Transition.Final(finalState);
        var operandAndOperatorTransition = Transition.OperandAndOperatorTransition(one);
        var operandAndClosingParenthesisTransition = Transition.OperandAndClosingParenthesisTransition(nine);
        
        one.AddStateForOpeningParenthesis(new Transition(one, Transition.StackActionPush, Transition.LexemeActionOperatorRecognized));
        nine.AddStateForClosingParenthesis(new Transition(nine, Transition.StackActionPop, Transition.LexemeActionOperatorRecognized));

        var nineTransition = new Transition(one, lexemeAction: Transition.LexemeActionOperatorRecognized);
        nine.AddStateForPlus(nineTransition);
        nine.AddStateForMultiply(nineTransition);
        nine.AddStateForNull(new Transition(finalState, Transition.StackActionEmpty));
       
        var twoTransition = new Transition(two, lexemeAction: Transition.LexemeActionAddChar);

        two.AddStateForSymbols('A', 'Z', twoTransition);
        two.AddStateForSymbols('a', 'z', twoTransition);
        two.AddStateForSymbols('0', '9', twoTransition);

        two.AddStateForEquals(operandAndOperatorTransition);
        
        var elevenTransition = new Transition(two, lexemeAction: Transition.LexemeActionAddChar);
        var eleven = State.Eleven();

        eleven.AddStateForSymbols('A', 'Z', elevenTransition);
        eleven.AddStateForSymbols('a', 'z', elevenTransition);

        var five = State.Five();
        
        one.AddStateForZero(new Transition(five, lexemeAction: Transition.LexemeActionAddChar));

        var eigth = State.Eigth();
        var eigthTransition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);

        eigth.AddStateForSymbols('0', '9', eigthTransition);
        eigth.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);
        eigth.AddStateForPlus(operandAndOperatorTransition);
        eigth.AddStateForMultiply(operandAndOperatorTransition);
        eigth.AddStateForNull(finalTransition);
        
        
        var seven = State.Seven();
        var sevenTransition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);

        seven.AddStateForSymbols('1', '9', sevenTransition);

        var six = State.Six();
        var sixTransition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);

        six.AddStateForSymbols('1', '9', sixTransition);

        sixTransition = new Transition(seven, lexemeAction: Transition.LexemeActionAddChar);
       
        six.AddStateForPlus(sixTransition);
        six.AddStateForMinus(sixTransition);
        
        
        var ten = State.Ten();
        var tenTransition = new Transition(ten, lexemeAction: Transition.LexemeActionAddChar);
        
        five.AddState('.', tenTransition);

        ten.AddStateForSymbols('0', '9', tenTransition);
        
        ten.AddState('E', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        ten.AddState('e', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        
        ten.AddState(')', operandAndClosingParenthesisTransition);
        
        ten.AddState('+', operandAndOperatorTransition);
        ten.AddState('*', operandAndOperatorTransition);
        
        ten.AddState('\0', finalTransition);


        var four = State.Four();
        var fourTransition = new Transition(four, lexemeAction: Transition.LexemeActionAddChar);

        one.AddStateForSymbols('1', '9', fourTransition);
        four.AddStateForSymbols('0', '9', fourTransition);
      
        four.AddState('.', new Transition(ten, lexemeAction: Transition.LexemeActionAddChar));
        
        four.AddState('E', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        four.AddState('e', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        
        four.AddState(')', operandAndClosingParenthesisTransition);
        
        four.AddState('+', operandAndOperatorTransition);
        four.AddState('*', operandAndOperatorTransition);
        
        four.AddState('\0', finalTransition);
        
        var three = State.Three();
        var threeTransition = new Transition(three, lexemeAction: Transition.LexemeActionAddChar);

        one.AddStateForSymbols('A', 'Z', threeTransition);
        one.AddStateForSymbols('a', 'z', threeTransition);

        three.AddStateForSymbols('A', 'Z', threeTransition);
        three.AddStateForSymbols('a', 'z', threeTransition);
        three.AddStateForSymbols('0', '9', threeTransition);
        
        three.AddState(')', operandAndClosingParenthesisTransition);
        
        three.AddState('+', operandAndOperatorTransition);
        three.AddState('*', operandAndOperatorTransition);
        
        three.AddState('\0', finalTransition);

        return Translator.Create(eleven);
    }

    
}
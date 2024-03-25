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
        
        
        one.AddState('(', new Transition(one, Transition.StackActionPush, Transition.LexemeActionOperatorRecognized));
        nine.AddState(')', new Transition(nine, Transition.StackActionPop, Transition.LexemeActionOperatorRecognized));
        var transition = new Transition(one, lexemeAction: Transition.LexemeActionOperatorRecognized);
        nine.AddState('+', transition);
        nine.AddState('*', transition);
        nine.AddState('\0', new Transition(finalState, Transition.StackActionEmpty));
       
        var twoTransition = new Transition(two, lexemeAction: Transition.LexemeActionAddChar);

        for (var symbol = 'A'; symbol <= 'Z'; symbol++)
            two.AddState(symbol, twoTransition);

        for (var symbol = 'a'; symbol <= 'z'; symbol++)
            two.AddState(symbol, twoTransition);

        for (var symbol = '0'; symbol <= '9'; symbol++)
            two.AddState(symbol, twoTransition);

        two.AddState('=', operandAndOperatorTransition);
        
        var elevenTransition = new Transition(two, lexemeAction: Transition.LexemeActionAddChar);
        var eleven = State.Eleven();

        for (var symbol = 'A'; symbol <= 'Z'; symbol++)
            eleven.AddState(symbol, elevenTransition);

        for (var symbol = 'a'; symbol <= 'z'; symbol++)
            eleven.AddState(symbol, elevenTransition);

        var five = State.Five();
        
        one.AddState('0', new Transition(five, lexemeAction: Transition.LexemeActionAddChar));


        var eigth = State.Eigth();
        var eigthTransition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);

        for (var symbol = '0'; symbol <= '9'; symbol++)
            eigth.AddState(symbol, eigthTransition);

        eigth.AddState(')', operandAndClosingParenthesisTransition);
        
        eigth.AddState('+', operandAndOperatorTransition);
        eigth.AddState('*', operandAndOperatorTransition);
        
        eigth.AddState('\0', finalTransition);
        
        
        var seven = State.Seven();
        var sevenTransition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);

        for (var symbol = '1'; symbol <= '9'; symbol++)
            seven.AddState(symbol, sevenTransition);

        var six = State.Six();
        var sixTransition = new Transition(eigth, lexemeAction: Transition.LexemeActionAddChar);

        for (var symbol = '1'; symbol <= '9'; symbol++)
            six.AddState(symbol, sixTransition);

        sixTransition = new Transition(seven, lexemeAction: Transition.LexemeActionAddChar);
       
        six.AddState('+', sixTransition);
        six.AddState('-', sixTransition);
        
        
        var ten = State.Ten();
        var tenTransition = new Transition(ten, lexemeAction: Transition.LexemeActionAddChar);
        
        five.AddState('.', tenTransition);
        for (var symbol = '0'; symbol <= '9'; symbol++)
            ten.AddState(symbol, tenTransition);
        
        ten.AddState('E', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        ten.AddState('e', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        
        ten.AddState(')', operandAndClosingParenthesisTransition);
        
        ten.AddState('+', operandAndOperatorTransition);
        ten.AddState('*', operandAndOperatorTransition);
        
        ten.AddState('\0', finalTransition);


        var four = State.Four();
        var fourTransition = new Transition(four, lexemeAction: Transition.LexemeActionAddChar);

        for (var symbol = '1'; symbol <= '9'; symbol++)
            one.AddState(symbol, fourTransition);

        for (var symbol = '0'; symbol <= '9'; symbol++)
            four.AddState(symbol, fourTransition);
       
        four.AddState('.', new Transition(ten, lexemeAction: Transition.LexemeActionAddChar));
        
        four.AddState('E', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        four.AddState('e', new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        
        four.AddState(')', operandAndClosingParenthesisTransition);
        
        four.AddState('+', operandAndOperatorTransition);
        four.AddState('*', operandAndOperatorTransition);
        
        four.AddState('\0', finalTransition);
        
        
        
        var three = State.Three();
        var threeTransition = new Transition(three, lexemeAction: Transition.LexemeActionAddChar);

        for (var symbol = 'A'; symbol <= 'Z'; symbol++)
            one.AddState(symbol, threeTransition);

        for (var symbol = 'a'; symbol <= 'z'; symbol++)
            one.AddState(symbol, threeTransition);

        for (var symbol = 'A'; symbol <= 'Z'; symbol++)
            three.AddState(symbol, threeTransition);

        for (var symbol = 'a'; symbol <= 'z'; symbol++)
            three.AddState(symbol, threeTransition);

        for (var symbol = '0'; symbol <= '9'; symbol++)
            three.AddState(symbol, threeTransition);
        
        three.AddState(')', operandAndClosingParenthesisTransition);
        
        three.AddState('+', operandAndOperatorTransition);
        three.AddState('*', operandAndOperatorTransition);
        
        three.AddState('\0', finalTransition);


        return new Translator(new PushdownAutomaton(eleven), new NameParser(), new MarshallingYardAlgorithm(), new Optimizer(), new Logger());
    }
}
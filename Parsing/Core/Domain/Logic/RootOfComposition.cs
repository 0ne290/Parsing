using Parsing.Core.Domain.Data.StateMachine;

namespace Parsing.Core.Domain.Logic;

public class RootOfComposition
{
    public Translator CreateTranslator()
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

        five.AddStateForDecimalPoint(tenTransition);

        ten.AddStateForSymbols('0', '9', tenTransition);

        ten.AddStateForExponent(new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        ten.AddStateForLn(new Transition(six, lexemeAction: Transition.LexemeActionAddChar));

        ten.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);

        ten.AddStateForPlus(operandAndOperatorTransition);
        ten.AddStateForMultiply(operandAndOperatorTransition);

        ten.AddStateForNull(finalTransition);


        var four = State.Four();
        var fourTransition = new Transition(four, lexemeAction: Transition.LexemeActionAddChar);

        one.AddStateForSymbols('1', '9', fourTransition);
        four.AddStateForSymbols('0', '9', fourTransition);

        four.AddStateForDecimalPoint(new Transition(ten, lexemeAction: Transition.LexemeActionAddChar));

        four.AddStateForExponent(new Transition(six, lexemeAction: Transition.LexemeActionAddChar));
        four.AddStateForLn(new Transition(six, lexemeAction: Transition.LexemeActionAddChar));

        four.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);

        four.AddStateForPlus(operandAndOperatorTransition);
        four.AddStateForMultiply(operandAndOperatorTransition);

        four.AddStateForNull(finalTransition);

        var three = State.Three();
        var threeTransition = new Transition(three, lexemeAction: Transition.LexemeActionAddChar);

        one.AddStateForSymbols('A', 'Z', threeTransition);
        one.AddStateForSymbols('a', 'z', threeTransition);

        three.AddStateForSymbols('A', 'Z', threeTransition);
        three.AddStateForSymbols('a', 'z', threeTransition);
        three.AddStateForSymbols('0', '9', threeTransition);

        three.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);

        three.AddStateForPlus(operandAndOperatorTransition);
        three.AddStateForMultiply(operandAndOperatorTransition);

        three.AddStateForNull(finalTransition);

        return Translator.Create(eleven);
    }
}
using Parsing.Core.Domain.Data.StateMachine;
using System.Threading;

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


        var oneTransition = new Transition(one, Transition.StackActionPush, Transition.LexemeActionOperatorRecognized);
        var nineTransition = new Transition(nine, Transition.StackActionPop, Transition.LexemeActionOperatorRecognized);

        one.AddStateForOpeningParenthesis(oneTransition);
        nine.AddStateForClosingParenthesis(nineTransition);

        nineTransition = new Transition(one, lexemeAction: Transition.LexemeActionOperatorRecognized);
        nine.AddStateForPlus(nineTransition);
        nine.AddStateForMultiply(nineTransition);

        nineTransition = new Transition(finalState, Transition.StackActionEmpty);
        nine.AddStateForNull(nineTransition);

        var twoTransition = Transition.CreateLexemeActionAddChar(two);

        two.AddStateForSymbols('A', 'Z', twoTransition);
        two.AddStateForSymbols('a', 'z', twoTransition);
        two.AddStateForSymbols('0', '9', twoTransition);

        two.AddStateForEquals(operandAndOperatorTransition);

        var elevenTransition = Transition.CreateLexemeActionAddChar(two);
        var eleven = State.Eleven();

        eleven.AddStateForSymbols('A', 'Z', elevenTransition);
        eleven.AddStateForSymbols('a', 'z', elevenTransition);

        var five = State.Five();
        var fiveTransition = Transition.CreateLexemeActionAddChar(five);

        one.AddStateForZero(fiveTransition);

        var eigth = State.Eigth();
        var eigthTransition = Transition.CreateLexemeActionAddChar(eigth);

        eigth.AddStateForSymbols('0', '9', eigthTransition);
        eigth.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);
        eigth.AddStateForPlus(operandAndOperatorTransition);
        eigth.AddStateForMultiply(operandAndOperatorTransition);
        eigth.AddStateForNull(finalTransition);

        var seven = State.Seven();
        var sevenTransition = Transition.CreateLexemeActionAddChar(eigth);

        seven.AddStateForSymbols('1', '9', sevenTransition);

        var six = State.Six();
        var sixTransition = Transition.CreateLexemeActionAddChar(eigth);

        six.AddStateForSymbols('1', '9', sixTransition);

        sixTransition = Transition.CreateLexemeActionAddChar(seven);

        six.AddStateForPlus(sixTransition);
        six.AddStateForMinus(sixTransition);

        var ten = State.Ten();
        var tenTransition = Transition.CreateLexemeActionAddChar(ten);

        five.AddStateForDecimalPoint(tenTransition);

        ten.AddStateForSymbols('0', '9', tenTransition);

        tenTransition = Transition.CreateLexemeActionAddChar(six);
        ten.AddStateForExponent(tenTransition);
        ten.AddStateForLn(tenTransition);

        ten.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);

        ten.AddStateForPlus(operandAndOperatorTransition);
        ten.AddStateForMultiply(operandAndOperatorTransition);

        ten.AddStateForNull(finalTransition);


        var four = State.Four();
        var fourTransition = Transition.CreateLexemeActionAddChar(four);

        one.AddStateForSymbols('1', '9', fourTransition);
        four.AddStateForSymbols('0', '9', fourTransition);

        fourTransition = Transition.CreateLexemeActionAddChar(ten);
        four.AddStateForDecimalPoint(fourTransition);

        fourTransition = Transition.CreateLexemeActionAddChar(six);
        four.AddStateForExponent(fourTransition);
        four.AddStateForLn(fourTransition);

        four.AddStateForClosingParenthesis(operandAndClosingParenthesisTransition);

        four.AddStateForPlus(operandAndOperatorTransition);
        four.AddStateForMultiply(operandAndOperatorTransition);

        four.AddStateForNull(finalTransition);

        var three = State.Three();
        var threeTransition = Transition.CreateLexemeActionAddChar(three);

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
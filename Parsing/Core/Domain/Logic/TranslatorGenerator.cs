using Parsing.Core.Domain.Data.StateMachine;
using System.Threading;

namespace Parsing.Core.Domain.Logic;

public class TranslatorGenerator
{
    public Translator Create()
    {
        var one = State.One();
        var two = State.Two();
        var three = State.Three();
        var four = State.Four();
        var five = State.Five();
        var six = State.Six();
        var seven = State.Seven();
        var eigth = State.Eigth();
        var nine = State.Nine();
        var ten = State.Ten();
        var eleven = State.Eleven();
        var final = State.Final();

        OneGenerator(one, five);
        NineGenerator(one, nine, final);
        SixGenerator(six, seven);
        FiveGenerator(five, ten);
        TenGenerator(six, ten);
        FourGenerator(four, six, ten);
        CreateLexemeActionAddCharTransitionGenerator(one, two, three, four, six, seven, eigth, ten, eleven);
        OperandAndClosingParenthesisTransitionGenerator(three, four, eigth, nine, ten);
        OperandAndOperatorTransitiongenerator(one, two, three, four, eigth, ten);
        FinalGenerator(three, four, eigth, ten, final);

        return Translator.Create(eleven);
    }

    void OneGenerator(State one, State five)
    {
        var oneTransition = new Transition(one, Transition.StackActionPush, Transition.LexemeActionOperatorRecognized);
        var fiveTransition = Transition.CreateLexemeActionAddChar(five);

        one.AddStateForOpeningParenthesis(oneTransition);
        one.AddStateForZero(fiveTransition);
    }

    void NineGenerator(State one, State nine, State final)
    {
        var oneTransition = new Transition(one, lexemeAction: Transition.LexemeActionOperatorRecognized);
        var nineTransition = new Transition(nine, Transition.StackActionPop, Transition.LexemeActionOperatorRecognized);
        var finalTransition = new Transition(final, Transition.StackActionEmpty);

        nine.AddStateForPlus(oneTransition);
        nine.AddStateForMultiply(oneTransition);

        nine.AddStateForClosingParenthesis(nineTransition);

        nine.AddStateForNull(finalTransition);
    }

    void SixGenerator(State six, State seven)
    {
        var sixTransition = Transition.CreateLexemeActionAddChar(seven);

        six.AddStateForPlus(sixTransition);
        six.AddStateForMinus(sixTransition);
    }

    public void FiveGenerator(State five, State ten)
    {
        var tenTransition = Transition.CreateLexemeActionAddChar(ten);

        five.AddStateForDecimalPoint(tenTransition);
    }

    void TenGenerator(State six, State ten)
    {
        var transition = Transition.CreateLexemeActionAddChar(six);
        
        ten.AddStateForExponent(transition);
        ten.AddStateForLn(transition);
    }
    
    void FourGenerator(State four, State six, State ten)
    {
        var transitionSix = Transition.CreateLexemeActionAddChar(six);
        var transitionTen = Transition.CreateLexemeActionAddChar(ten);

        four.AddStateForDecimalPoint(transitionTen);

        four.AddStateForExponent(transitionSix);
        four.AddStateForLn(transitionSix);
    }

    public void CreateLexemeActionAddCharTransitionGenerator(State one, State two, State three, State four, State six, State seven, State eigth, State ten, State eleven)
    {
        var twoTransition = Transition.CreateLexemeActionAddChar(two);
        var threeTransition = Transition.CreateLexemeActionAddChar(three);
        var fourTransition = Transition.CreateLexemeActionAddChar(four);
        var sixTransition = Transition.CreateLexemeActionAddChar(eigth);
        var sevenTransition = Transition.CreateLexemeActionAddChar(eigth);
        var eigthTransition = Transition.CreateLexemeActionAddChar(eigth);
        var tenTransition = Transition.CreateLexemeActionAddChar(ten);
        var elevenTransition = Transition.CreateLexemeActionAddChar(two);

        one.AddStateForSymbols('A', 'Z', threeTransition);
        one.AddStateForSymbols('a', 'z', threeTransition);
        one.AddStateForSymbols('1', '9', fourTransition);

        two.AddStateForSymbols('A', 'Z', twoTransition);
        two.AddStateForSymbols('a', 'z', twoTransition);
        two.AddStateForSymbols('0', '9', twoTransition);

        three.AddStateForSymbols('A', 'Z', threeTransition);
        three.AddStateForSymbols('a', 'z', threeTransition);
        three.AddStateForSymbols('0', '9', threeTransition);

        four.AddStateForSymbols('0', '9', fourTransition);

        six.AddStateForSymbols('1', '9', sixTransition);
        
        seven.AddStateForSymbols('1', '9', sevenTransition);
        
        eigth.AddStateForSymbols('0', '9', eigthTransition);

        ten.AddStateForSymbols('0', '9', tenTransition);

        eleven.AddStateForSymbols('A', 'Z', elevenTransition);
        eleven.AddStateForSymbols('a', 'z', elevenTransition);
    }

    void OperandAndClosingParenthesisTransitionGenerator(State three, State four, State eigth, State nine, State ten)
    {
        var transition = Transition.OperandAndClosingParenthesisTransition(nine);
        
        three.AddStateForClosingParenthesis(transition);
        four.AddStateForClosingParenthesis(transition);
        eigth.AddStateForClosingParenthesis(transition);
        ten.AddStateForClosingParenthesis(transition);
    }

    void OperandAndOperatorTransitiongenerator(State one, State two, State three, State four, State eigth, State ten)
    {
        var transition = Transition.OperandAndOperatorTransition(one);
        
        two.AddStateForEquals(transition);
        three.AddStateForPlus(transition);
        three.AddStateForMultiply(transition);
        four.AddStateForPlus(transition);
        four.AddStateForMultiply(transition);
        eigth.AddStateForPlus(transition);
        eigth.AddStateForMultiply(transition);
        ten.AddStateForPlus(transition);
        ten.AddStateForMultiply(transition);
    }

    void FinalGenerator(State three, State four, State eigth, State ten, State final)
    {
        var transition = Transition.Final(final);

        three.AddStateForNull(transition);
        four.AddStateForNull(transition);
        eigth.AddStateForNull(transition);
        ten.AddStateForNull(transition);
    }
}
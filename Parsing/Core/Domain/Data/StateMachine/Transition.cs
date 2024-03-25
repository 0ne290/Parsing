using Parsing.Core.Domain.Data.Syntax;

namespace Parsing.Core.Domain.Data.StateMachine;

public class Transition
{
    public Transition(State state, Action<Stack<char>>? stackAction = null, Action<List<Token>, List<char>, char>? lexemeAction = null)
    {
        State = state;
        StackAction = stackAction ?? (_ => { });
        LexemeAction = lexemeAction ?? ((_, _, _) => { });
    }

    public static Transition Final(State finalState) => new(finalState, StackActionEmpty, LexemeActionOperandRecognized);

    public static Transition OperandAndOperatorTransition(State one) => new(one, lexemeAction: LexemeActionOperandAndOperatorRecognized);

    public static Transition OperandAndClosingParenthesisTransition(State nine) => new(nine, StackActionPop, LexemeActionOperandAndOperatorRecognized);
    public static Transition CreateLexemeActionAddChar(State state) => new Transition(state, lexemeAction: LexemeActionAddChar);

    public State State { get; }
    
    public Action<Stack<char>> StackAction { get; }
    
    public Action<List<Token>, List<char>, char> LexemeAction { get; }

    public static Action<Stack<char>> StackActionPush { get; } = stack => stack.Push('+');

    public static Action<Stack<char>> StackActionPop { get; } = stack =>
    {
        if (stack.Pop() != '+')
            throw new Exception("Expected '+' character on stack");
    };
    
    public static Action<Stack<char>> StackActionEmpty { get; } = stack =>
    {
        if (stack.Count > 0)
            throw new Exception("The stack was expected to be empty");
    };
    
    public static Action<List<Token>, List<char>, char> LexemeActionAddChar { get; } = (_, lexeme, character) => lexeme.Add(character);
    
    public static Action<List<Token>, List<char>, char> LexemeActionOperatorRecognized { get; } =
        (tokens, _, character) =>
        {
            tokens.Add(new Token(character.ToString()));
        };
    
    public static Action<List<Token>, List<char>, char> LexemeActionOperandRecognized { get; } =
        (tokens, currentToken, _) =>
        {
            tokens.Add(new Token(new string(currentToken.ToArray())));
        };

    public static Action<List<Token>, List<char>, char> LexemeActionOperandAndOperatorRecognized { get; } =
        (tokens, currentToken, character) =>
        {
            tokens.Add(new Token(new string(currentToken.ToArray())));
            tokens.Add(new Token(character.ToString()));
            currentToken.Clear();
        };
}
namespace Parsing;

public class PushdownAutomaton
{
    public PushdownAutomaton(State initialState) => _currentState = initialState;
    
    public List<Token> Parse(char[] inputString)
    {
        try
        {
            while (!_currentState.IsFinal)
            {
                var transition = _currentState.ExecuteTransition(inputString[_inputStringIndex]);
                transition.StackAction(_stack);
                transition.LexemeAction(_tokens, _currentToken, inputString[_inputStringIndex]);
                _currentState = transition.State;
                _inputStringIndex++;
            }

            return new List<Token>(_tokens);
        }
        catch (Exception)
        {
            throw new Exception($"{_inputStringIndex}");
        }
    }
    
    private State _currentState;

    private readonly Stack<char> _stack = new();
    
    private readonly List<char> _currentToken = new();
    
    private readonly List<Token> _tokens = new();

    private int _inputStringIndex;
}
namespace Parsing;

public class State
{
    public State(bool isFinal = false) => IsFinal = isFinal;

    public Transition ExecuteTransition(char charFromInputString) => _transitions[charFromInputString];

    public void AddState(char charFromInputString, Transition transition) =>
        _transitions.Add(charFromInputString, transition);
    
    public bool IsFinal { get; }

    private readonly Dictionary<char, Transition> _transitions = new();
}
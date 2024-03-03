namespace Parsing.Core.Domain.Data.StateMachine;

public class State
{
    public State(string name, bool isFinal = false)
    {
        Name = name;
        IsFinal = isFinal;
    }

    public Transition ExecuteTransition(char charFromInputString) => _transitions[charFromInputString];

    public void AddState(char charFromInputString, Transition transition) =>
        _transitions.Add(charFromInputString, transition);
    
    public bool IsFinal { get; }
    
    public string Name { get; }

    private readonly Dictionary<char, Transition> _transitions = new();
}
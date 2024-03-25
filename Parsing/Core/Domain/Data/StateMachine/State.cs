namespace Parsing.Core.Domain.Data.StateMachine;

public class State
{
    public State(string name, bool isFinal = false)
    {
        Name = name;
        IsFinal = isFinal;
    }

    public static State One() => new State("one");
    public static State Two() => new State("two");
    public static State Three() => new State("three");
    public static State Four() => new State("four");
    public static State Five() => new State("five");
    public static State Six() => new State("six");
    public static State Seven() => new State("seven");
    public static State Eigth() => new State("eigth");
    public static State Nine() => new State("nine");
    public static State Ten() => new State("ten");
    public static State Eleven() => new State("eleven");
    public static State Final() => new State("final", true);


    public Transition ExecuteTransition(char charFromInputString) => _transitions[charFromInputString];

    public void AddState(char charFromInputString, Transition transition) =>
        _transitions.Add(charFromInputString, transition);
    
    public bool IsFinal { get; }
    
    public string Name { get; }

    private readonly Dictionary<char, Transition> _transitions = new();
}
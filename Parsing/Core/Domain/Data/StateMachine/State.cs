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

    public void AddStateForPlus(Transition transition) => _transitions.Add('+', transition);
    public void AddStateForMinus(Transition transition) => _transitions.Add('-', transition);
    public void AddStateForMultiply(Transition transition) => _transitions.Add('*', transition);
    public void AddStateForEquals(Transition transition) => _transitions.Add('=', transition);
    public void AddStateForZero(Transition transition) => _transitions.Add('0', transition);
    public void AddStateForNull(Transition transition) => _transitions.Add('\0', transition);
    public void AddStateForLn(Transition transition) => _transitions.Add('E', transition);

    public void AddStateForOpeningParenthesis(Transition transition) => _transitions.Add('(', transition);
    public void AddStateForClosingParenthesis(Transition transition) => _transitions.Add(')', transition);

    public void AddState(char charFromInputString, Transition transition) =>
        _transitions.Add(charFromInputString, transition);

    public void AddStateForSymbols(char begin, char end, Transition transition)
    {
        for (var symbol = begin; symbol <= end; symbol++)
            _transitions.Add(symbol, transition);
    }

    public bool IsFinal { get; }
    
    public string Name { get; }

    private readonly Dictionary<char, Transition> _transitions = new();
}
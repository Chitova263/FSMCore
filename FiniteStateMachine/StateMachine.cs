namespace FiniteStateMachine;

public class StateMachine<TState,  TTrigger> 
    where TState : Enum 
    where TTrigger : Enum
{
    private readonly Dictionary<RuleSetKey<TState, TTrigger>, Rule<TState, TTrigger>> _rules;
    public IReadOnlyDictionary<RuleSetKey<TState, TTrigger>, Rule<TState, TTrigger>> Rules => _rules;
    public TState CurrentState { get; private set; }
    private StateMachine(TState initialState, Dictionary<RuleSetKey<TState, TTrigger>, Rule<TState, TTrigger>> rules)
    {
        _rules = rules;
        CurrentState = initialState;
    }
    
    public class Builder
    {
        private readonly HashSet<Rule<TState, TTrigger>> _rules = new();
        private TState? _initialState;
        
        private Builder()
        {
            
        }
        
        public static Builder Create()
        {
            return new Builder();
        }
        
        public Builder HasInitialState(TState initialState)
        {
            _initialState = initialState;
            return this;
        }
        
        public Builder AddTransition(Rule<TState, TTrigger> rule)
        {
            _rules.Add(rule);
            return this;
        }
        
        public StateMachine<TState, TTrigger> Build()
        {
            if(_initialState is null)
                throw new InvalidOperationException("Initial state must not be null.");
            
            var rules = _rules.ToDictionary(
                rule => new RuleSetKey<TState, TTrigger> { From = rule.From, Trigger = rule.Trigger }, 
                rule => rule
                );
            return new StateMachine<TState, TTrigger>(_initialState, rules);
        }
    }

    public bool Trigger(TTrigger trigger)
    {
        var key = new RuleSetKey<TState, TTrigger>
        {
            From = CurrentState,
            Trigger = trigger
        };
        if (!_rules.TryGetValue(key, out var rule)) return false;
        CurrentState = rule.To;
        return true;
    }
}
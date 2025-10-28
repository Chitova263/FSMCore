namespace FiniteStateMachine;

public sealed class FiniteStateMachineBuilder<TState, TTrigger>
    where TTrigger : Enum
    where TState : notnull
{
    private readonly TState _initialState;
    private readonly Dictionary<TState, Dictionary<TTrigger, TState>> _transitions = new();
        
    internal FiniteStateMachineBuilder(TState initialState)
    {
        _initialState = initialState;
    }
    
    public FiniteStateMachineBuilder<TState, TTrigger> State(
        TState state,
        Action<StateConfiguration<TState, TTrigger>> configure)
    {
        var cfg = new StateConfiguration<TState, TTrigger>(state);
        configure(cfg);
        _transitions.Add(state, cfg.Transitions);
        return this;
    }
    
    public StateMachine<TState, TTrigger> Build()
    {
        var rules = GetTransitionRules();
        return new StateMachine<TState, TTrigger>(_initialState, rules);
    }

    private Dictionary<RuleKey<TState, TTrigger>, Rule<TState, TTrigger>> GetTransitionRules()
    {
        Dictionary<RuleKey<TState,TTrigger>,Rule<TState,TTrigger>> rules = [];
        foreach (var (fromState, transitions) in _transitions)
        {
            foreach (var (trigger, targetState) in transitions)
            {
                var ruleKey = new RuleKey<TState, TTrigger>
                {
                    From = fromState,
                    Trigger = trigger,
                };
                var rule = new Rule<TState, TTrigger>
                {
                    From = fromState,
                    To = targetState,
                    Trigger = trigger,
                };
                rules.Add(ruleKey, rule);
            }
        }
        return rules;
    }
}

public sealed class StateConfiguration<TState, TTrigger> where TTrigger : Enum
{
    private readonly TState _state;
    internal Dictionary<TTrigger, TState> Transitions { get; } = new();

    internal StateConfiguration(TState state)
    {
        _state = state;
    }
    
    public TransitionConfiguration<TState, TTrigger> On(TTrigger trigger)
    {
        return new TransitionConfiguration<TState, TTrigger>(this, trigger);
    }
}
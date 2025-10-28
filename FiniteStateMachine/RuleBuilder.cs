namespace FiniteStateMachine;

public class RuleBuilder<TState, TTrigger> where TState : Enum where TTrigger : Enum
{
    private TState? From { get; set; }
    private TState? To { get; set; }
    private TTrigger? Trigger { get; set; }

    private RuleBuilder()
    {
    }
    
    public static RuleBuilder<TState, TTrigger> Builder()
    {
        return new RuleBuilder<TState, TTrigger>();
    }

    public RuleBuilder<TState, TTrigger> InState(TState state)
    {
        From = state;
        return this;
    }

    public RuleBuilder<TState, TTrigger> TransitionTo(TState state)
    {
        To = state;
        return this;
    }

    public RuleBuilder<TState, TTrigger> OnTrigger(TTrigger trigger)
    {
        Trigger = trigger;
        return this;
    }

    public Rule<TState, TTrigger> Build()
    {
        if(From is null)
            throw new Exception("From must not be null");
        
        if(To is null)
            throw new Exception("To must not be null");
        if(Trigger is null)
            throw new Exception("Trigger must not be null");
        
        return new Rule<TState, TTrigger>
        {
            From = From,
            To = To,
            Trigger = Trigger
        };
    }
}
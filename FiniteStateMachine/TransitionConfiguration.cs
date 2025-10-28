namespace FiniteStateMachine;

public sealed class TransitionConfiguration<TState, TTrigger> where TTrigger : Enum
{
    private readonly StateConfiguration<TState, TTrigger> _stateConfiguration;
    private readonly TTrigger _trigger;

    internal TransitionConfiguration(StateConfiguration<TState, TTrigger> stateConfiguration, TTrigger trigger)
    {
        _stateConfiguration = stateConfiguration;
        _trigger = trigger;
    }
    
    public StateConfiguration<TState, TTrigger> GoTo(TState target)
    {
        _stateConfiguration.Transitions[_trigger] = target;
        return _stateConfiguration;
    }
}
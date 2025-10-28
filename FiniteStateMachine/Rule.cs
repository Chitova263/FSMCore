namespace FiniteStateMachine;

public record Rule<TState, TTrigger> 
    where TState : Enum 
    where TTrigger : Enum
{
    public required TState From { get; init; }
    public required TState To { get; init; }
    public required TTrigger Trigger { get; init; }
}
namespace FiniteStateMachine;

public record RuleSetKey<TState, TTrigger> where TState : Enum where TTrigger : Enum
{
    public required TState From { get; init; }
    public required TTrigger Trigger { get; init; }
};
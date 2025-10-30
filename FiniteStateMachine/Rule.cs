﻿namespace FiniteStateMachine;

public sealed record Rule<TState, TTrigger> 
    where TTrigger : Enum
{
    public required TState From { get; init; }
    public required TState To { get; init; }
    public required TTrigger Trigger { get; init; }
}
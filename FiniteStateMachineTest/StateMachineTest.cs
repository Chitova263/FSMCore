using FiniteStateMachine;

namespace FiniteStateMachineTest;

public class StateMachineTest
{
    [Fact]
    public void ShouldCreateFiniteStateMachine()
    {
        var stateMachine = BuildStateMachine();
        var stateMachineCurrentState = stateMachine.CurrentState;
        Assert.Equal(9, stateMachine.Rules.Count);
        Assert.Equal(State.Paused, stateMachineCurrentState);
    }


    [Fact]
    public void ShouldTransitionToNextState()
    {
        var stateMachine = BuildStateMachine();
        var result = stateMachine.Trigger(Trigger.Play);
        Assert.True(result);
        Assert.Equal(State.Playing, stateMachine.CurrentState);
        
    }
    
    private static StateMachine<State, Trigger> BuildStateMachine()
    {
        return StateMachine<State, Trigger>.Builder
            .Create()
            .HasInitialState(State.Paused)
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Paused).OnTrigger(Trigger.Play).TransitionTo(State.Playing).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Paused).OnTrigger(Trigger.Pause).TransitionTo(State.Paused).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Paused).OnTrigger(Trigger.Stop).TransitionTo(State.Stopped).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Stopped).OnTrigger(Trigger.Play).TransitionTo(State.Playing).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Stopped).OnTrigger(Trigger.Pause).TransitionTo(State.Stopped).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Stopped).OnTrigger(Trigger.Stop).TransitionTo(State.Stopped).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Playing).OnTrigger(Trigger.Play).TransitionTo(State.Playing).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Playing).OnTrigger(Trigger.Pause).TransitionTo(State.Paused).Build())
            .AddTransition(RuleBuilder<State, Trigger>.Builder().InState(State.Playing).OnTrigger(Trigger.Stop).TransitionTo(State.Stopped).Build())
            .Build();
    }
}
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
        Assert.Equal(EnumState.Paused, stateMachineCurrentState);
    }
    
    [Fact]
    public void ShouldTransitionToNextState()
    {
        var stateMachine = BuildStateMachine();
        var result = stateMachine.Trigger(Trigger.Play);
        Assert.True(result);
        Assert.Equal(EnumState.Playing, stateMachine.CurrentState);
        
    }
    
    private static StateMachine<EnumState, Trigger> BuildStateMachine()
    {
         return StateMachine<EnumState, Trigger>
            .WithInitialState(EnumState.Paused)
            .State(EnumState.Paused, cfg => cfg
                .On(Trigger.Play).GoTo(EnumState.Playing)
                .On(Trigger.Pause).GoTo(EnumState.Paused)
                .On(Trigger.Stop).GoTo(EnumState.Stopped)
            )
            .State(EnumState.Stopped, cfg => cfg
                .On(Trigger.Play).GoTo(EnumState.Playing)
                .On(Trigger.Pause).GoTo(EnumState.Stopped)
                .On(Trigger.Stop).GoTo(EnumState.Stopped)  
            )
            .State(EnumState.Playing, cfg => cfg
                .On(Trigger.Play).GoTo(EnumState.Playing)
                .On(Trigger.Pause).GoTo(EnumState.Paused)
                .On(Trigger.Stop).GoTo(EnumState.Stopped)  
            )
            .Build();
    }
}
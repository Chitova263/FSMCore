# StateCore

The library provides a flexible framework for creating finite state machines in C#.

## Features

- Define states and triggers.
- Create state machines with customizable transitions.
- Easily integrate into your projects for handling state-driven logic.

### Basic Usage

To get started with the `FiniteStateMachine` library, you need to define your states and triggers as enums:

```csharp
public enum State
{
    Paused,
    Playing,
    Stopped
}

public enum Trigger
{
    Play,
    Pause,
    Stop
}
```

Building a State Machine

```csharp
 var statemachine = StateMachine<EnumState, Trigger>
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
```

Once the state machine is built, you can trigger transitions based on current state:
```csharp
var result = stateMachine.Trigger(Trigger.Play);
Console.WriteLine(stateMachine.CurrentState); // Outputs: Playing
```




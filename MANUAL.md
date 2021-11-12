# Hyper Casual Starter Project Manual

## Getting started

## GameStateMachine events

`GameStateMachine` events have to be called by user.

The list of all public events:

- `GameLoaded` – invokes on the first game laod. Invokes from HyperCasualSDK
- `LevelFinished` – should be invoked each time level is ended
- `Play` – should be invoked every time a gameplay session begins
- `IncreaseLevelScore` – should be invoked when current level score is increased: for example, player picks up a coin
- `PerfectlyIncreaseLevelScore` – invoke it if you want to show Achievement message
- `CheckpointReached` – if your game has checkpoints use this event
- `Revive` – invoke it on player revive (for example, after watching Rewarded Video Ad)
- `PlayerSucceed` – should be invoked when player successfully finishes the level 
- `PlayerLost` – should be invoked when player lost
- `RestartLevel` – should be invoked when user requests a restart

Events should be invoked by user:

```c#
GameStateMachine.Events.LevelFinished
GameStateMachine.Events.Play
GameStateMachine.Events.IncreaseLevelScore
GameStateMachine.Events.PerfectlyIncreaseLevelScore
GameStateMachine.Events.CheckpointReached
GameStateMachine.Events.Revive
GameStateMachine.Events.PlayerSucceed 
GameStateMachine.Events.PlayerLost
GameStateMachine.Events.RestartLevel
```

Events for subscribe only:

```c#
GameStateMachine.Events.GameLoaded
```

## InputController events

There three events in InputController:

- `InputController.Events.TouchBegin` invokes when user starts to interact
- `InputController.Events.TouchMoveDelta` invokes with `delta` parameter which indicates how far user moved its finger along X axis
- `InputController.Events.TouchEndDelta` invokes with `delta` parameter which is final value of distance between touch down and touch up events

Example of adding a listener to the events:

```c#
InputController.Events.TouchBegin.AddListener(() => _xOnTouchDown = transform.position.x);
InputController.Events.TouchMoveDelta.AddListener(Slide);
```

Please, avoid to invoke `InputController` events in your code. 

## Audio Assistant usage

- Add `AudioAssistant` prefab on your scene from `HyperCasualSDK/Prefabs`
- It contains `AudioAssistant` and `AudioSource` components on it
- There is editable dictionary with of all sounds in the game
- You may want to add more sound effect types. You can find corresponding enum in `AudioAssistant.cs`
- Play the sound using static method:
```c#
AudioSource.Play(SoundEffectType.Button);
```

## Control camera

- Add `Cinemachine` prefab on your scene from `HyperCasualSDK/Prefabs`
- Cinemachine's parameters `Follow` and `LookAt` are set to one object `Target` in the prefab
- Control your camera changing Transform of `Target` object: for example, you may add `Follow.cs` component to the `Target` object and set it's target property to Player's object

## Include your advertisement SDK in the project

1. Inherit a class from the abstract class `AbstractAdsPresenter`
2. Implement its abstract methods calling corresponding methods in the ads SDK
3. Add the component to a game object
4. Link the game object in Unity Editor on HyperCasualSDK object in `AdsController` script section

## Common recommendations

- Disable UI game object in the Editor during gameplay development
- Consider as a rule to subscribe to events in `Awake()` and invoke events not earlier than in `Start()`. It would help to avoid 

# AutoReference
![](https://img.shields.io/badge/unity-2022.3+-000.svg)

## Description
AutoReference is designed to solve and optimize the efficient access to Unity Components.

## Table of Contents
- [Getting Started](#Getting-Started)
    - [Install manually (using .unitypackage)](#Install-manually-(using-.unitypackage))
    - [Install via UPM (using Git URL)](#Install-via-UPM-(using-Git-URL))
- [Project Structure](#Project-Structure)
    - [Interfaces](#Interfaces)
- [License](#License)

## Getting Started
Prerequisites:
- [GIT](https://git-scm.com/downloads)
- [Unity](https://unity.com/releases/editor/archive) 2022.3+

### Install manually (using .unitypackage)
1. Download the .unitypackage from [releases](https://github.com/DanilChizhikov/AddressableManagement/releases/) page.
2. Open AutoReference.x.x.x.unitypackage

### Install via UPM (using Git URL)
1. Navigate to your project's Packages folder and open the manifest.json file.
2. Add this line below the "dependencies": { line
    - ```json title="Packages/manifest.json"
      "com.danilchizhikov.autoreference": "https://github.com/DanilChizhikov/AutoReference.git,
      ```
UPM should now install the package.

## Project Structure

### Interfaces

1. IBehaviourService
```csharp
public interface IBehaviourService
{
    IBehaviourState CreateState(IBehaviourStateConfig config);
    IBehaviourAction CreateAction(IBehaviourActionConfig config);
    IBehaviourDecision CreateDecision(IBehaviourDecisionConfig config);
}
```

2. IBehaviourGraph
```csharp
public interface IBehaviourGraph
{
    bool IsPlaying { get; }
    
    void Enter();
    void Update();
    void Exit();
}
```

```csharp
public interface IBehaviourGraphConfig
{
    IBehaviourStateConfig EnterState { get; }
    IReadOnlyList<IBehaviourStateConfig> States { get; }
}
```

3. IBehaviourState
```csharp
public interface IBehaviourState
{
    void Enter();
    void Update();
    bool TryGetNextState(out IBehaviourState nextState);
    void Exit();
}
```

```csharp
public interface IBehaviourStateConfig { }
```
 - IBehaviourLogicState
```csharp
public interface IBehaviourLogicState
{
    IReadOnlyList<IBehaviourAction> Actions { get; }
    IReadOnlyList<IBehaviourTransition> Transitions { get; }
}
```

```csharp
public interface IBehaviourLogicStateConfig
{
    IReadOnlyList<IBehaviourActionConfig> Actions { get; }
    IReadOnlyList<IBehaviourTransitionConfig> Transitions { get; }
}
```

```csharp
public interface IBehaviourAction
{
    void Enter();
    void Processing();
    void Exit();
}
```

```csharp
public interface IBehaviourActionConfig { }
```

```csharp
public interface IBehaviourTransition
{
    IBehaviourPort TruePort { get; }
    IBehaviourPort FalsePort { get; }
    IReadOnlyList<IBehaviourDecision> Decisions { get; }
}
```

```csharp
public interface IBehaviourTransitionConfig
{
    IBehaviourPortConfig TruePort { get; }
    IBehaviourPortConfig FalsePort { get; }
    IReadOnlyList<IBehaviourDecisionConfig> Decisions { get; }
}
```

 - IBehaviourForkState
```csharp
public interface IBehaviourForkState
{
    IReadOnlyList<IBehaviourFork> Forks { get; }
}
```

```csharp
public interface IBehaviourForkStateConfig
{
    IReadOnlyList<IBehaviourForkConfig> ForkInfos { get; }
}
```

```csharp
public interface IBehaviourFork
{
    IBehaviourPort Port { get; }
    int Weight { get; }
    IReadOnlyList<IBehaviourDecision> Decisions { get; }
}
```

```csharp
public interface IBehaviourForkConfig
{
    IBehaviourPortConfig Port { get; }
    int Weight { get; }
    IReadOnlyList<IBehaviourDecisionConfig> Decisions { get; }
}
```

4. IBehaviourPort
```csharp
public interface IBehaviourPort
{
    IBehaviourState NextState { get; }
}
```

```csharp
public interface IBehaviourPortConfig
{
    string Name { get; }
    IBehaviourStateConfig NextState { get; }
}
```

5. Factories
```csharp
public interface IBehaviourStateFactory : IServiceable
{
    IBehaviourState Create(IBehaviourStateConfig config);
}
```

````csharp
public interface IBehaviourActionFactory : IServiceable
{
    IBehaviourAction Create(IBehaviourActionConfig config);
}
````

```csharp
public interface IBehaviourDecisionFactory : IServiceable
{
    IBehaviourDecision Create(IBehaviourDecisionConfig config);
}
```

6. IBehaviourGraphBuilder
```csharp
public interface IBehaviourGraphBuilder
{
    IBehaviourGraph Build();
    void Reset();
}
```

## License

MIT
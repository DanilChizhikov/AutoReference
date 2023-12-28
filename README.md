# AutoReference
![](https://img.shields.io/badge/unity-2022.3+-000.svg)

## Description
AutoReference is designed to solve and optimize the efficient access to Unity Components.

## Table of Contents
- [Getting Started](#Getting-Started)
    - [Install manually (using .unitypackage)](#Install-manually-(using-.unitypackage))
    - [Install via UPM (using Git URL)](#Install-via-UPM-(using-Git-URL))
- [Problems Addressed](#Problems-Addressed)
- [Using](#Using)
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

## Problems Addressed

- Frequent use of 'GetComponent' can significantly reduce performance.
```csharp
private void Update()
{
    var rigidbody = GetComponent<Rigidbody>();
    // Performing some operations with rigidbody...
}
```

- Using 'GetComponent' in 'Awake' or 'Start' methods for one-time initialization of all component fields might lead to lengthy and confusing code.
It's also easy to forget to initialize new fields.
```csharp
private Animator animator;
private Rigidbody rigidbody;
private Collider collider;
private AudioSource audioSource;
// Many other fields...

private void Awake()
{
   animator = GetComponent<Animator>();
   rigidbody = GetComponent<Rigidbody>();
   collider = GetComponent<Collider>();
   audioSource = GetComponent<AudioSource>();
    // More GetComponent calls...
}
```

- Implementing lazy-loading with properties might lead to verbose and redundant code.
```csharp
private Animator _animator;
private RigidBody _rigidbody;

public Animator animator
{
    get
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        return _animator;
    }
}

public RigidBody rigidbody
{
    get
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<RigidBody>();
        }
        return _rigidbody;
    }
}
```

## Using
- MonoBehaviour
```csharp
using UnityEngine;
using MbsCore.AutoReference;

public class ExampleBehaviour : MonoBehaviour
{
    [SerializeField, MonoAutoReference(true)] private Rigidbody[] _rigidbodies;
    [SerializeField, MonoAutoReference] private Rigidbody _rigidbodie;
    [SerializeField, ScriptableAutoReference] private ExampleScriptableObject _scriptableObject;
}
```

- ScriptableObject
```csharp
public class ExampleScriptableObject : ScriptableObject
{
    [SerializeField, ScriptableAutoReference] private ExampleInfo[] _infos;
    [SerializeField, ScriptableAutoReference] private ExampleInfo _defaultInfo;
}

public class ExampleInfo : ScriptableObject
{
    [SerializeField] private string _id;
}
```

## License

MIT
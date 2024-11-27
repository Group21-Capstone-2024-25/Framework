using UnityEngine;

/// <summary>
/// IActionStrategy
/// </summary>
// TODO Migrate Strategies, Beliefs, Actions and Goals to Scriptable Objects and create Node Editor for them
public interface IActionStrategy
{
    bool CanPerform { get; }
    bool Complete { get; }

    void Start()
    {
        // noop
    }

    void Update(float deltaTime)
    {
        // noop
    }

    void Stop()
    {
        // noop
    }
}
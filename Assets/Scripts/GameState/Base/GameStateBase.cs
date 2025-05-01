using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public abstract class GameStateBase : MonoBehaviour
{
    [SerializeField] public States NextState;
    
    public static Action<States> ChangeStateRequested;
    public InputAction nextStateAction;
    
    public bool is_connected = false;
    
    
    public virtual void StartState()
    {
        AddListeners();
    }

    public virtual void AddListeners() { }

    public virtual void RemoveListeners() { }

    public virtual void ExitState()
    {
        RemoveListeners();
    }
    
    public IEnumerator DisposeActionNextFrame()
    {
        yield return null;
        nextStateAction?.Dispose();
    }
}

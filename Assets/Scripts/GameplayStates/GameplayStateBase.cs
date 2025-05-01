using System;
using UnityEngine;

public class GameplayStateBase : MonoBehaviour
{
    [SerializeField] public GameplayStates NextState;
    public static Action<GameplayStates> gameplayStateRequested;
    public bool is_connected = false;
    
    
    public virtual void StartState()
    {
        AddListeners();
    }
    
    public virtual void Dependences()
    {
    }
    
    public virtual void AddListeners()
    {
    }

    public virtual void RemoveListeners()
    {
    }


    public virtual void ExitState()
    {
        RemoveListeners();
    }
}

using UnityEngine;
using System;

public class ObstacleBase : MonoBehaviour
{
    public bool isActivated = false;
    public static Action ObstacleActivated;
    
    public virtual void Initialize()
    {
        AddListeners();
    }
    
    
    public virtual void AddListeners()
    {
    }

    public virtual void OnActivated()
    {
    }

    public virtual void RemoveListeners()
    {
    }


    public virtual void Conclude()
    {
        RemoveListeners();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class InteractableBase : MonoBehaviour
{
    public static Action<Vector2, float ,InteractableBase> actionPressed;
    

    public virtual void Initialize()
    {
       
    }
    
    public virtual void OnNpcButtonPressed()
    {
       
    }
    public virtual void Action()
    {
       
    }
    public virtual void Conclude()
    {
       
    }
}

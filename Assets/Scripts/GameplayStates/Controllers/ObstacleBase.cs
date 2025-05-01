using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    public bool isActivated = false;
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

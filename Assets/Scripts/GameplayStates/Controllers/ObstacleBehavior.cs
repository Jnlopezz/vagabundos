using UnityEngine;
using UnityEngine.UI;

public class ObstacleBehavior : ObstacleBase
{
    private Button button;
    

    public override void AddListeners()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnActivated);
    }
    
    public override void OnActivated()
    {
        isActivated = true;
        Conclude();
    }
    
    public override void RemoveListeners()
    {
        button.onClick.RemoveListener(OnActivated);
    }
}

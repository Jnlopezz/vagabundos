using UnityEngine;

public class LevelController : MonoBehaviour
{
    //[SerializeField] public ObstacleManager obstaclesManager;
    [SerializeField] public StarGameplayController starGameplayController;
    
    public void Initialize()
    {
        //obstaclesManager.Initialize();
        starGameplayController.Initialize();
    }
}

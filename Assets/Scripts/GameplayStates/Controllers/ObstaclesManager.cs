using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private ObstacleBehavior[] obstacles;

    public void Initialize()
    {
        obstacles = GetComponentsInChildren<ObstacleBehavior>(includeInactive: true);

        foreach (var obstacle in obstacles)
        {
            obstacle.Initialize();
        }

    }
    
   
    
    public void Conclude()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.Conclude();
        }

    }
}

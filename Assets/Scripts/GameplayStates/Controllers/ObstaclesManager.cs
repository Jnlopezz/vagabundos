using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class ObstacleManager : MonoBehaviour
{
    private ObstacleBehavior[] obstacles;
    public static Action<Vector2> obstaclePressed;
    [SerializeField] private Canvas canvas;

    public void Initialize()
    {
        canvas.worldCamera = Camera.main;
        obstacles = GetComponentsInChildren<ObstacleBehavior>(includeInactive: true);

        foreach (var obstacle in obstacles)
        {
            obstacle.Initialize();
            ObstacleBehavior.ObstacleActivated += OnObstacleActivated;
        }

    }

    private void OnObstacleActivated()
    {
        
    }


    public void Conclude()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.Conclude();
            ObstacleBehavior.ObstacleActivated -= OnObstacleActivated;
        }

    }
}

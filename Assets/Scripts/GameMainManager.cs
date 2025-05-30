using UnityEngine;
using DavidFDev.DevConsole;

public class GameMainManager : MonoBehaviour
{
    [SerializeField] private GameSceneController gameSceneController;
    
    
    private void Awake()
    {
        gameSceneController.StartGame();
        
        //DevConsole.EnableConsole();
        //DevConsole.OpenConsole();
        DevConsole.Log("Hello world!");
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action<GameStateBase> GetState;
    
    [SerializeField] private States InitState;
    private States currentState = States.None;
    private GameStateBase currentStateScene;

    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        OnChangeState(InitState);
    }
    

    private void OnChangeState(States nextState)
    {
        if (currentState != States.None)
        {
            RemoveListeners();
            SceneManager.UnloadSceneAsync(currentState.ToString());
        }

        Debug.Log(nextState.ToString());
        SceneManager.LoadSceneAsync(nextState.ToString(), LoadSceneMode.Additive).completed += (_) =>
        {
            OnSceneChangeComplete();
        };
        
        currentState = nextState;
    }

    private void OnSceneChangeComplete()
    {
        Scene currentScene = SceneManager.GetSceneByName(currentState.ToString());
        foreach (var root in currentScene.GetRootGameObjects())
        {
            currentStateScene = root.GetComponent<GameStateBase>();
            break;
        }
        
        AddListeners();

    }

    private void AddListeners()
    {
        if (currentStateScene != null)
        {
            currentStateScene.StartState();
            GameStateBase.OnChangeStateRequested += OnChangeState;
        }
        
    }
    
    private void RemoveListeners()
    {
        if (currentStateScene != null)
        {
            currentStateScene.ExitState();
            GameStateBase.OnChangeStateRequested -= OnChangeState;
        }
    }
    

    private void OnApplicationQuit()
    {
        RemoveListeners();
    }
}

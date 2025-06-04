using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private States InitState;
    private States currentState = States.None;
    private GameStateBase currentStateScene;
    

    public void StartGame()
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

        currentStateScene = null;
        currentState = nextState;
        SceneManager.LoadSceneAsync(nextState.ToString(), LoadSceneMode.Additive).completed += (_) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentState.ToString()));
            OnSceneChangeComplete();
        };
        
        
    }

    private void OnSceneChangeComplete()
    {
        Scene currentScene = SceneManager.GetSceneByName(currentState.ToString());
        currentStateScene = FindObjectOfType<GameStateBase>();

        AddListeners();

    }

    private void AddListeners()
    {
        if (currentStateScene != null)
        {
            currentStateScene.StartState();
            GameStateBase.ChangeStateRequested += OnChangeState;
        }
        
    }
    
    private void RemoveListeners()
    {
        if (currentStateScene != null)
        {
            currentStateScene.ExitState();
            GameStateBase.ChangeStateRequested -= OnChangeState;
        }
    }
    

    private void OnApplicationQuit()
    {
        RemoveListeners();
    }
}

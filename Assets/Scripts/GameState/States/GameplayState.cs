using System;
using UnityEngine;


public class GamePlayState : GameStateBase
{
    private GameplayStates gameplayState = GameplayStates.Enter;
    [SerializeField] private EnterLevelState enterLevelState;
    [SerializeField] private ExplorationState explorationState;
    [SerializeField] private RunState runState;
    [SerializeField] private LoseLevelState loseLevelState;
    
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InputGameplay inputGameplay;
    [SerializeField] private CharacterControllerPlayer characterControllerPlayer;
    private LevelMovementController levelMovementController;
    
    
    private GameplayStateBase currentStateObject = null;
    private InteractableBase interactableSelected;
    private bool is_wating_start = false;
    
    public override void StartState()
    {
        OnChangeGameState(gameplayState);
        AddListeners();
    }
    
    public override void AddListeners()
    {
        if (is_connected)
            return;
        
        is_connected = true;
        currentStateObject.gameplayStateRequested += OnChangeGameState;
        levelManager.levelLoaded += OnLevelLoaded;
        inputGameplay.InputClickActivated += OnInputGamePressed;
        InteractableBase.actionPressed += OnActionPressed;
    }
    
    public void OnLevelLoaded()
    {
        levelMovementController = levelManager.currentLevelInstance.GetComponent<LevelMovementController>();
        levelMovementController.rotationFinished += OnRotationFinished;
        levelMovementController.rotationStarted += OnRotationStarted;
        enterLevelState.nextGameplayState();
        characterControllerPlayer.Initialize();
        levelManager.Initialize();
    }

    public void OnInputGamePressed(Vector2 clickPosition)
    {
        if (is_wating_start) { return; }

        Transform levelTransform = levelManager.currentLevelInstance.transform;
        levelMovementController.RotateLevel(clickPosition, levelTransform);
    }

    private void OnRotationStarted(int rotationdirection)
    {
        characterControllerPlayer.SetCharacterDirection(rotationdirection);
        characterControllerPlayer.ChangeCharacterAction(Animations.Run);
    }

    private void OnRotationFinished()
    {
        if (is_wating_start)
        {
            interactableSelected?.Action();
            interactableSelected = null;
            characterControllerPlayer.ChangeCharacterAction(Animations.Idle);
            //OnChangeGameState(GameplayStates.Run);
            is_wating_start = false;
            return;
        }

        characterControllerPlayer.ChangeCharacterAction(Animations.Idle);
    }

    private void OnActionPressed(Vector2 clickPosition, InteractableBase npc)
    {
        interactableSelected = npc;
        OnInputGamePressed(clickPosition);
        is_wating_start = true;
    }

    private void OnChangeGameState(GameplayStates nextState)
    {
        currentStateObject?.ExitState();
        gameplayState = nextState;

        switch (gameplayState)
        {
            case GameplayStates.Enter:
                enterLevelState.Dependences(levelManager);
                currentStateObject = enterLevelState;
                break;
            case GameplayStates.Exploration:
                explorationState.Dependences(inputGameplay);
                currentStateObject = explorationState;
                break;
            case GameplayStates.Run:
                currentStateObject = runState;
                break;
            case GameplayStates.Ending:
                currentStateObject = loseLevelState;
                break;
            default:
                Debug.LogWarning("Estado de juego desconocido: " + gameplayState);
                currentStateObject = null;
                break;
        }
        
        Debug.Log(gameplayState.ToString());
        currentStateObject.StartState();
        
    }


    public override void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        currentStateObject.gameplayStateRequested -= OnChangeGameState;
        levelManager.levelLoaded -= OnLevelLoaded;
        inputGameplay.InputClickActivated -= OnInputGamePressed;
        levelMovementController.rotationFinished -= OnRotationFinished;
        levelMovementController.rotationStarted -= OnRotationStarted;
        InteractableBase.actionPressed -= OnActionPressed;
    }
    
}

using UnityEngine;


public class GamePlayState : GameStateBase
{
    private GameplayStates gameplayState = GameplayStates.Enter;
    [SerializeField] private EnterLevelState enterLevelState;
    [SerializeField] private ExplorationState explorationState;
    [SerializeField] private RunState runState;
    [SerializeField] private LoseLevelState loseLevelState;
    
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InputGameplay inputGameplayState;
    [SerializeField] private CharacterController characterController;
    private LevelMovementController levelMovementController;
    
    
    private GameplayStateBase currentStateObject = null;
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
        GameplayStateBase.gameplayStateRequested += OnChangeGameState;
        LevelManager.levelLoaded += OnLevelLoaded;
        InputGameplay.InputActivated += OnInputGamePressed;
        StarGameplayController.startPressed += OnStartPressed;
    }
    
    public void OnLevelLoaded()
    {
        levelMovementController = levelManager.currentLevelInstance.GetComponent<LevelMovementController>();
        LevelMovementController.rotationFinished += OnRotationFinished;
        enterLevelState.nextGameplayState();
        characterController.Initialize();
        levelManager.Initialize();
    }

    public void OnInputGamePressed(Vector2 position)
    {
        Vector2 direction = position - new Vector2(653, 814);
        Transform levelTransform = levelManager.currentLevelInstance.transform;
        levelMovementController.RotateLevel(position, levelTransform);
        characterController.SetCharacterDirection(direction);
        characterController.ChangeCharacterAction(Animations.Run);
    }

    private void OnRotationFinished()
    {
        if (is_wating_start)
        {
            characterController.ChangeCharacterAction(Animations.Idle);
            OnChangeGameState(GameplayStates.Run);
            return;
        }

        characterController.ChangeCharacterAction(Animations.Idle);
    }

    private void OnStartPressed()
    {
        is_wating_start = true;
        StarGameplayController.startPressed -= OnStartPressed;
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
                explorationState.Dependences(inputGameplayState);
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
        GameplayStateBase.gameplayStateRequested -= OnChangeGameState;
        LevelManager.levelLoaded -= OnLevelLoaded;
        InputGameplay.InputActivated -= OnInputGamePressed;
        LevelMovementController.rotationFinished -= OnRotationFinished;
    }
    
}

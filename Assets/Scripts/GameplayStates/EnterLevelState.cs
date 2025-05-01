


public class EnterLevelState : GameplayStateBase
{
    private LevelManager levelManager;
    private string current_level = "Level-01";
    
    public override void StartState()
    {
        AddListeners();
        levelManager.LoadLevel(current_level);
    }

    public void Dependences(LevelManager controller)
    {
        levelManager = controller;
    }

    public override void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
    }

    public void nextGameplayState()
    {
        gameplayStateRequested?.Invoke(NextState);
    }

    public override void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
    }
}

using UnityEngine;

public class ExplorationState : GameplayStateBase
{
    private InputGameplay inputGameplayState;
    public override void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        inputGameplayState.AddListeners();
    }
    
    public void Dependences(InputGameplay input)
    {
        inputGameplayState = input;
    }

    public override void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        inputGameplayState.RemoveListeners();
    }
}

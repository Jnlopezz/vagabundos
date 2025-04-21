using UnityEngine.InputSystem;

public class GamePlayState : GameStateBase
{
    public override void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        nextStateAction = new InputAction(binding: "<Keyboard>/n");
        nextStateAction.performed += ctx => RequestStateChange();
        nextStateAction.Enable();
    }

    public override void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        nextStateAction?.Disable();
        StartCoroutine(DisposeActionNextFrame());
    }
    
}

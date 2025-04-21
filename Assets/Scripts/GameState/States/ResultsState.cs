using UnityEngine.InputSystem;

public class ResultsState : GameStateBase
{
    public override void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        nextStateAction = new InputAction(binding: "<Keyboard>/x");
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

using UnityEngine.InputSystem;

public class MainState : GameStateBase
{
    public override void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        nextStateAction = new InputAction(binding: "<Mouse>/leftButton");
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

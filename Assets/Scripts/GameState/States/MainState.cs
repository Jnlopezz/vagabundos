using UnityEngine;
using UnityEngine.InputSystem;

public class MainState : GameStateBase
{
    [SerializeField] private InputMainState inputMainState;
    
    
    
    public override void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        inputMainState.InputActivated += OnInputPressed;
        inputMainState.AddListeners();
    }
    
    private void OnInputPressed()
    {
        ChangeStateRequested(NextState);
    }

    public override void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        inputMainState.InputActivated += OnInputPressed;
        inputMainState.RemoveListeners();
        StartCoroutine(DisposeActionNextFrame());
    }
}

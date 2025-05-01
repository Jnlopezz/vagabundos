using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputMainState : MonoBehaviour
{
    public InputAction inputPressed;
    private bool is_connected = false;
    public event Action InputActivated;

    public void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        inputPressed = new InputAction(binding: "<Mouse>/leftButton");
        inputPressed.performed += ctx => InputActivated?.Invoke();
        inputPressed.Enable();
    }

    public void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        inputPressed?.Disable();
        inputPressed.performed -= ctx => InputActivated?.Invoke();
    }
}

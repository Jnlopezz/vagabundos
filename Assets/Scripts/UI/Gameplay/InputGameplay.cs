using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class InputGameplay : MonoBehaviour
{
    
    private bool is_connected = false;
    public static event Action<Vector2> InputActivated;
    public InputAction inputGamePressed;

    public void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        inputGamePressed = new InputAction(binding: "<Mouse>/leftButton");
        inputGamePressed.performed += OnInputPerformed;
        inputGamePressed.Enable();
    }

    private void OnInputPerformed(InputAction.CallbackContext context)
    {
        Vector2 clickPosition = Mouse.current.position.ReadValue();
        InputActivated?.Invoke(clickPosition);
    }

    public void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        inputGamePressed.performed -= OnInputPerformed;
        inputGamePressed.Disable();
    }
}

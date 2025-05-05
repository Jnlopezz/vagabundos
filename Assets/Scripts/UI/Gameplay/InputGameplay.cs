using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.EventSystems;
public class InputGameplay : MonoBehaviour
{
    
    private bool is_connected = false;
    public event Action<Vector2> InputClickActivated;
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
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        Vector2 clickPosition = Mouse.current.position.ReadValue();
        InputClickActivated?.Invoke(clickPosition);
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

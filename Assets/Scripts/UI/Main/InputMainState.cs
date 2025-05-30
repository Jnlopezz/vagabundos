using UnityEngine;
using UnityEngine.UI;
using System;

public class InputMainState : MonoBehaviour
{
    public Button StartButton;
    private bool is_connected = false;
    public event Action InputActivated;

    public void AddListeners()
    {
        if (is_connected)
            return;

        is_connected = true;
        StartButton.onClick.AddListener(OnButtonClicked);
    }

    public void RemoveListeners()
    {
        if (!is_connected)
            return;

        is_connected = false;
        StartButton.onClick.RemoveListener(OnButtonClicked);
    }
    
    private void OnButtonClicked()
    {
        InputActivated?.Invoke();
    }
}

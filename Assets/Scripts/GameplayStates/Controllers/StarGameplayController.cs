using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;


public class StarGameplayController : MonoBehaviour
{
    public static Action<Vector2> startPressed;
    [SerializeField] private Button startButton;
    [SerializeField] private Canvas canvas;

    public void Initialize()
    {
        canvas.worldCamera = Camera.main;
        startButton.onClick.AddListener(OnStartButtonPressed);
    }
    
    public void OnStartButtonPressed()
    {
        Vector2 clickPosition = Mouse.current.position.ReadValue();
        startPressed?.Invoke(clickPosition);
    }
    
    public void Conclude()
    {
        startButton.onClick.RemoveListener(OnStartButtonPressed);
    }
}

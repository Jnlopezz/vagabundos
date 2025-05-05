using UnityEngine;
using UnityEngine.UI;
using System;

public class StarGameplayController : MonoBehaviour
{
    public static Action startPressed;
    [SerializeField] private Button startButton;
    [SerializeField] private Canvas canvas;

    public void Initialize()
    {
        print("inicializó boton");
        canvas.worldCamera = Camera.main;
        startButton.onClick.AddListener(OnStartButtonPressed);
    }
    
    public void OnStartButtonPressed()
    {
        print("invocó señal");
        startPressed?.Invoke();
    }
    
    public void Conclude()
    {
        startButton.onClick.RemoveListener(OnStartButtonPressed);
    }
}

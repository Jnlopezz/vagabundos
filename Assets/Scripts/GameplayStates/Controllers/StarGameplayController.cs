using UnityEngine;
using UnityEngine.UI;
using System;

public class StarGameplayController : MonoBehaviour
{
    public static Action startPressed;
    [SerializeField] private Button startButton;

    public void Initialize()
    {
        startButton.onClick.AddListener(OnStartButtonPressed);
    }
    
    public void OnStartButtonPressed()
    {
        startPressed?.Invoke();
    }
    
    public void Conclude()
    {
        startButton.onClick.RemoveListener(OnStartButtonPressed);
    }
}

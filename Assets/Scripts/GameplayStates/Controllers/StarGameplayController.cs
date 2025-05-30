using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;


public class StarGameplayController : MonoBehaviour
{
    public static Action<Vector2> startPressed;
    [SerializeField] private Button startButton;
    [SerializeField] private Canvas canvas;
    [SerializeField] private NpcView npcCharacter;
    [SerializeField] private GameObject greatings;

    public void Initialize()
    {
        greatings.SetActive(false);
        canvas.worldCamera = Camera.main;
        startButton.onClick.AddListener(OnStartButtonPressed);
    }
    
    public void OnStartButtonPressed()
    {
        Vector2 clickPosition = Mouse.current.position.ReadValue();
        startPressed?.Invoke(clickPosition);
    }

    public void NpcAction()
    {
        npcCharacter.PlayAnimation(Animations.Activated);
        greatings.SetActive(true);
    }

    public void Conclude()
    {
        startButton.onClick.RemoveListener(OnStartButtonPressed);
    }
}

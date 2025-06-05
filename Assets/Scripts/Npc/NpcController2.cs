using UnityEngine;
using UnityEngine.UI;
using System;
public class NpcController2 : InteractableBase
{
    [SerializeField] private Button npcButton;
    [SerializeField] private Canvas canvas;
    [SerializeField] private NpcView npcCharacter;
    [SerializeField] private GameObject clickGameObject;
    [SerializeField] private Transform npc_position;
    
    public override void Initialize()
    {
        canvas.worldCamera = Camera.main;
        npcButton.onClick.AddListener(OnNpcButtonPressed);
        clickGameObject.SetActive(false);
    }
    
    public override void OnNpcButtonPressed()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(npc_position.position);
        Vector2 npcScreenPosition = new Vector2(screenPosition.x, screenPosition.y);
        
        actionPressed?.Invoke(screenPosition, npc_position.localScale.x, this);
       
    }
    
    public override void Action()
    {
        npcCharacter.PlayAnimation(Animations.Activated);
        clickGameObject.SetActive(true);
       
    }
    
    public override void Conclude()
    {
        npcButton.onClick.RemoveListener(OnNpcButtonPressed);
    }
}

using System;
using UnityEngine;

[Serializable]
public class PlayerEntity
{
    public Action UpdatedScore;
    
    [SerializeField] private int score = 0;
    public int Score { get { return score; } }
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdatedScore?.Invoke();
    }
}

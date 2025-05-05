using UnityEngine;

public class CharacterControllerPlayer : MonoBehaviour
{
    [SerializeField] private CharacterView characterView;

    public void Initialize()
    {
        characterView.PlayAnimation(Animations.Idle);
    }


    public void ChangeCharacterAction(Animations animEnum)
    {
        characterView.PlayAnimation(animEnum);
    }

    public void SetCharacterDirection(int direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void Conclude()
    {
        
    }
}

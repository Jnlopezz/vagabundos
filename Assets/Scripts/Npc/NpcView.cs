using UnityEngine;
using System.Collections;

public class NpcView : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Animations currentAnim = Animations.Idle;


    public void PlayAnimation(Animations animation)
    {
        currentAnim = animation;
        

        switch (animation)
        {
            case Animations.Idle:
                animator.Play("Idle");
                animator.SetBool("is_activated", false);
                break;

            case Animations.Activated:
                animator.SetBool("is_activated", true);
                break;
            
            default:
                Debug.LogWarning("Animación no manejada: ");
                break;
        } 
    }

    private IEnumerator PlayAndReturn(string animationName, string toAnim )
    {
        animator.Play(animationName);
        
        yield return new WaitUntil(() =>
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
        });

        animator.Play(toAnim);
    }
}
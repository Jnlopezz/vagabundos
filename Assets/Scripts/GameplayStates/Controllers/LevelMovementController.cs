using UnityEngine;
using System;
using DG.Tweening;
using System;

[RequireComponent(typeof(Transform))]
public class LevelMovementController : MonoBehaviour
{
    [Header("Duración del tween (seg)")]
    [SerializeField] private float tweenDuration = 0.5f;
    public event Action rotationFinished;
    public event Action<int> rotationStarted;
    private Tween currentRotationTween;
    

    public void RotateLevel(Vector2 screenPosition, Transform levelTransform)
    {
        NormalizeAngle(levelTransform.localEulerAngles.z);
        Vector2 screenAngle = screenPosition - new Vector2(960, 1080);
        float clickAngle = Mathf.Atan2(screenAngle.x, screenAngle.y) * Mathf.Rad2Deg;
        int rotationIntDirection = (clickAngle < 0) ? -1 : 1;
        float difference = 180f - Mathf.Abs(clickAngle);
        float target = levelTransform.localEulerAngles.z + difference * -rotationIntDirection;
        RotateMovement(levelTransform, target);
        rotationStarted?.Invoke(rotationIntDirection);
    }

    public void RotateMovement(Transform levelTransform, float target)
    {
        if (currentRotationTween != null && currentRotationTween.IsActive())
        {
            currentRotationTween.Kill();
        }

        currentRotationTween = levelTransform.DOLocalRotate(
                new Vector3(0, 0, target),
                tweenDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                rotationFinished?.Invoke();
                currentRotationTween = null;
            });
    }
    

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0)
            angle += 360f;
        return angle;
    }
}

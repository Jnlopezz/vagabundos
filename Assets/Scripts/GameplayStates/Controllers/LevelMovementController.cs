using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(Transform))]
public class LevelMovementController : MonoBehaviour
{
    [Header("Duración del tween (seg)")]
    [SerializeField] private float tweenDuration = 0.5f;
    public static Action rotationFinished;
    public Vector2 direccion;
    

    public void RotateLevel(Vector2 screenPosition, Transform levelTransform)
    {
        NormalizeAngle(levelTransform.localEulerAngles.z);
        direccion = screenPosition - new Vector2(653, 814);
        float clickAngle = Mathf.Atan2(direccion.x, direccion.y) * Mathf.Rad2Deg;
        float difference = clickAngle - 180f;
        float target = levelTransform.localEulerAngles.z + difference;

        RotateMovement(levelTransform, target);
    }

    public void RotateMovement(Transform levelTransform, float target)
    {
        levelTransform.DOLocalRotate(
            new Vector3(0, 0, target),
            tweenDuration).OnComplete(() =>
            {
                rotationFinished?.Invoke();
            }
        );
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0)
            angle += 360f;
        return angle;
    }
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FillerController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int maxSliders;
    [SerializeField] private float timeToFill;
    [SerializeField] private float timeToFillIncreaseFactor; //how much will decrease relative to the last slider increase factor
    [SerializeField] private GameObject interactPointPrefab;
    [SerializeField] private int minZoneFillToSpawnInteractPoint;
    [SerializeField] private int maxZoneFillToSpawnInteractPoint;
    [SerializeField] private int baseInteractPointQuantity;
    [SerializeField] private float interactZoneQuantityMultiplier; //how much will increase relative to the last spawned interact points
    [SerializeField] private float minDistanceBetweenPoints;
    [SerializeField] private RectTransform sliderBackground;
    [SerializeField] private float clickTolerance;
    [SerializeField] private Color correctClickColor;
    [SerializeField] private Color incorrectClickColor;
    [SerializeField] private CanvasGroup sliderCanvasGroup;
    private int currentSlider = 1;
    private List<float> spawnPointsLocations = new();
    private int spawnPointsCreated;
    private Dictionary<float, GameObject> spawnedPoints = new();
    private bool notEnoughSpace;
    private HashSet<float> animatedPoints = new();
    private bool isInitialized = false;
    
    void Start()
    {
        sliderCanvasGroup.alpha = 0;
    }
    
    void Update()
    {
        if (!isInitialized)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float clickValue = slider.value;

            float closestPoint = -1f;
            float smallestDistance = float.MaxValue;

            foreach (float point in spawnPointsLocations)
            {
                float distance = Mathf.Abs(clickValue - point);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestPoint = point;
                }
            }

            if (closestPoint >= 0f)
            {
                var clickedAtRightLocation = smallestDistance <= clickTolerance;
                
                if (spawnedPoints.TryGetValue(closestPoint, out GameObject pointGameObject))
                {
                    if (animatedPoints.Contains(closestPoint))
                        return;
                    
                    if (clickedAtRightLocation)
                        pointGameObject.transform.DOPunchScale(Vector3.one * 1.5f, .5f);
                    else
                        pointGameObject.transform.DOShakePosition(.5f, Vector3.one * 6, 15);
                    
                    pointGameObject.GetComponent<Image>().color = clickedAtRightLocation? correctClickColor : incorrectClickColor;
                    animatedPoints.Add(closestPoint);
                }
            }
        }
        
        foreach (var point in spawnPointsLocations)
        {
            if (animatedPoints.Contains(point))
                continue;

            // Si el slider ya pasó más allá del punto + tolerancia
            if (slider.value > point + clickTolerance)
            {
                if (spawnedPoints.TryGetValue(point, out GameObject go))
                {
                    go.transform.DOShakePosition(.5f, Vector3.one * 6, 15);
                    go.GetComponent<Image>().color = incorrectClickColor;
                    animatedPoints.Add(point);
                }
            }
        }
    }

    public void StartSlidersSequence()
    {
        isInitialized = true;
        GenerateSpawnPoints();
        
        if (currentSlider > maxSliders || notEnoughSpace)
            return;

        sliderCanvasGroup.alpha = 1;
        

        SpawnInteractPointsOnSlider();

        timeToFill = currentSlider == 1 ? timeToFill : timeToFill / timeToFillIncreaseFactor;
        slider.DOValue(slider.maxValue, timeToFill).SetEase(Ease.Linear).OnComplete(() =>
        {
            Reset(ResetComplete);
        });
    }

    private void ResetComplete()
    {
        currentSlider += 1;
        StartSlidersSequence(); //Repeat the whole process
    }
    

    private void GenerateSpawnPoints()
    {
        spawnPointsLocations?.Clear();
        spawnPointsCreated = Mathf.CeilToInt(currentSlider == 1
            ? baseInteractPointQuantity
            : spawnPointsCreated * interactZoneQuantityMultiplier);

        float min = minZoneFillToSpawnInteractPoint;
        float max = maxZoneFillToSpawnInteractPoint;
        float range = max - min;

        // Check if there's enough space to fit all points with the required minimum distance
        float requiredSpace = (spawnPointsCreated - 1) * minDistanceBetweenPoints;
        if (requiredSpace > range)
        {
            notEnoughSpace = true;
            Debug.LogWarning("Not enough space to place all spawn points with the minimum required distance.");
            return;
        }

        // Calculate the extra space available to distribute randomly between the points
        float extraSpace = range - requiredSpace;

        float[] positions = new float[spawnPointsCreated];
        float current = min + Random.Range(0f, extraSpace / spawnPointsCreated); // initial offset with some randomness

        for (int i = 0; i < spawnPointsCreated; i++)
        {
            positions[i] = current;
            spawnPointsLocations.Add(current);

            if (i < spawnPointsCreated - 1)
            {
                // Move to the next position with minimum spacing + a bit of random offset
                float randomOffset = Random.Range(0f, extraSpace / spawnPointsCreated);
                current += minDistanceBetweenPoints + randomOffset;
            }
        }
    }
    
    private void SpawnInteractPointsOnSlider()
    {
        foreach (float spawnValue in spawnPointsLocations)
        {
            // Normalize the spawn value between 0 and 1
            float normalizedValue = Mathf.InverseLerp(slider.minValue, slider.maxValue, spawnValue);

            // Get the full width of the slider's background area
            float width = sliderBackground.rect.width;

            // Calculate local X position
            float localX = normalizedValue * width;

            // Instantiate marker as child of the background (or any static container)
            GameObject marker = Instantiate(interactPointPrefab, sliderBackground.parent); // not fillRect!
            spawnedPoints.Add(spawnValue, marker);
            Vector2 anchoredPos = sliderBackground.anchoredPosition;
            anchoredPos.x += localX;
            marker.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchoredPos.x, sliderBackground.anchoredPosition.y);
        }
    }

    private void Reset(Action onComplete = null)
    {
        animatedPoints.Clear();
        foreach (var spawnedPoint in spawnedPoints)
        {
            spawnedPoint.Value.transform.DOScale(Vector3.zero, .1f).SetEase(Ease.OutBounce).OnComplete(() => Destroy(spawnedPoint.Value));
        }
        spawnedPoints.Clear();

        slider.DOValue(slider.minValue, .1f).SetEase(Ease.Linear).OnComplete( () => onComplete?.Invoke() );
    }
}

using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelManager : MonoBehaviour
{
    public Action levelLoaded;
    public Action EarthQuakeStarted;
    
    public LevelController levelController;
    public GameObject currentLevelInstance;
    [SerializeField] private FillerController earthquakeGameFillerController;

    public void Initialize()
    {
        levelController = currentLevelInstance.GetComponent<LevelController>();
        levelController.Initialize();
        levelController.LastLevelCompleted += OnLastLevelCompleted;
    }

    private void OnLastLevelCompleted()
    {
        EarthQuakeStarted?.Invoke();
        earthquakeGameFillerController.Initialize();
    }

    public void LoadLevel(string name_string)
    {
        if (currentLevelInstance != null)
        {
            Destroy(currentLevelInstance);
        }
        
        Addressables.LoadAssetAsync<GameObject>(name_string).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                currentLevelInstance = Instantiate(handle.Result, transform);
                currentLevelInstance.name = name_string;
                levelLoaded?.Invoke();
            }
            else
            {
                Debug.LogError("No se pudo cargar el addressable: " + name_string);
            }
        };
    }
    

    public void Conclude()
    {
        levelController.LastLevelCompleted -= OnLastLevelCompleted;
    }
}

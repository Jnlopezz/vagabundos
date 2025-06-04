using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelManager : MonoBehaviour
{
    public Action levelLoaded;
    
    public LevelController levelController;
    public GameObject currentLevelInstance;

    public void Initialize()
    {
        levelController = currentLevelInstance.GetComponent<LevelController>();
        levelController.Initialize();
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

    public void ActivateNpc()
    {
        levelController.ActivateNpc();
    }

    public void Conclude()
    {
        
    }
}

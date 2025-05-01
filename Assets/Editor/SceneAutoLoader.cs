#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class AlwaysOpenMainScene
{
    static AlwaysOpenMainScene()
    {
        EditorApplication.playModeStateChanged += LoadMainSceneOnPlay;
    }

    static void LoadMainSceneOnPlay(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene("Assets/Scenes/GameManager.unity");
            }
            else
            {
                EditorApplication.isPlaying = false;
            }
        }
    }
}
#endif
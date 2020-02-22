using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Text.RegularExpressions;

public static class AutoUpdateAllCubes
{

    private static List<SceneAsset> scenes;

    [MenuItem("Editing/Auto Update All Cubes")]
    static void UpdateAllCubes()
    {

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.SaveOpenScenes();
        }

        EditorBuildSettingsScene[] allScenes = EditorBuildSettings.scenes;
        Regex regex = new Regex("[0-9]");
        foreach (EditorBuildSettingsScene scene in allScenes)
        {
            if (regex.IsMatch(scene.path))
            {
                var currentScene = EditorSceneManager.OpenScene(scene.path);

                CubeChange cubeChange = GameObject.Find(Tags.CUBEUPDATER).GetComponent<CubeChange>();
                cubeChange.UpdateAllCubesInScene();

                EditorSceneManager.SaveScene(currentScene);
                EditorSceneManager.CloseScene(currentScene, true);
                AssetDatabase.SaveAssets();
            }
        }
    }

    [MenuItem("Editing/Save")]
    static void SaveTest()
    {
        LevelProgress.SaveCompletedLevel(EditorSceneManager.GetActiveScene().name);
    }

    [MenuItem("Editing/Load")]
    static void LoadTest()
    {
        foreach (string level in LevelProgress.LoadCompletedLevels())
        {
            Debug.Log("Level complete: " + level);
        }
    }
}

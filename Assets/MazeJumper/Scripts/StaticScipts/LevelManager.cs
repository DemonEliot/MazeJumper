using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager : GenericSingletonClass<LevelManager>
{
    private static List<string> completedLevels = new List<string>();

    public static List<string> GetCompletedLevels()
    {
        return completedLevels;
    }

    public static void SaveCompletedLevel(string level)
    {
        completedLevels.Add(level);
        SaveSystem.SaveGameData();
    }

    public static List<string> LoadCompletedLevels()
    {
        GameData gameData = SaveSystem.LoadGameData();
        completedLevels = gameData.GetCompletedLevels();
        return completedLevels;
    }

    public static void ResetLevelCompletion()
    {
        completedLevels = new List<string>();
    }

    public static void EndLevel(UI ui, string sceneName)
    {
        ui.LevelEndMenu();
        SaveCompletedLevel(sceneName);
    }
}

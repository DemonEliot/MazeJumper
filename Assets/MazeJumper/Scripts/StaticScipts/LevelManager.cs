using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GenericSingletonClass<LevelManager>
{
    private static int completedLevels = 5;

    public static int GetCompletedLevels()
    {
        return completedLevels;
    }

    public static void SaveCompletedLevel(int level)
    {
        if (level > completedLevels)
        {
            completedLevels = level;
            SaveSystem.SaveGameData();
        }
    }

    public static int LoadCompletedLevels()
    {
        GameData gameData = SaveSystem.LoadGameData();
        completedLevels = gameData.GetCompletedLevels();
        return completedLevels;
    }

    public static void ResetLevelCompletion()
    {
        completedLevels = 0;
    }

    public static void EndLevel(UI ui, int level)
    {
        ui.LevelEndMenu();
        SaveCompletedLevel(level);
    }
}

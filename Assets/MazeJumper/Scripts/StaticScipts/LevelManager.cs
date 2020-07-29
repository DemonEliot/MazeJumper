using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GenericSingletonClass<LevelManager>
{
    private static int completedLevels = 1;

    // Does this ever run? Class is static.. Meaning this is never attached to an object to 'start'
    private void Start()
    {
        LoadCompletedLevels();
    }

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

    public static void EndLevel(UI ui, string level)
    {
        level = StringUtils.SplitAndTrimSceneName(level);
        ui.LevelEndMenu();
        SaveCompletedLevel(int.Parse(level));
    }
}

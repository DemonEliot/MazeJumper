using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelProgress
{
    private static List<string> completedLevels;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private List<string> completedLevels;

    public GameData(List<string> levels)
    {
        completedLevels = levels;
    }

    public List<string> GetCompletedLevels()
    {
        return completedLevels;
    }
}

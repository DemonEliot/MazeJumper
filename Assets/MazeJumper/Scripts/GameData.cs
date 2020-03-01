using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private int completedLevels;

    public GameData(int levelsAlreadyCompleted)
    {
        completedLevels = levelsAlreadyCompleted;
    }

    public int GetCompletedLevels()
    {
        return completedLevels;
    }
}

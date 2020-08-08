using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeDifficulty 
{

    public static void CalculateMazeDifficulty(int mapSize, int gates)
    {
        List<string> difficultyList = GetDifficultyList();
        string mapDifficulty;
        string gateDifficulty;

        mapDifficulty = GetMapDifficulty(mapSize, difficultyList);
        gateDifficulty = GetGateDifficulty(gates, difficultyList);

        string totalDifficulty = WorkOutDifficultyTotal(mapDifficulty, gateDifficulty, difficultyList);
        Debug.Log("Difficulty of map is: " + totalDifficulty);
        
    }

    private static List<string> GetDifficultyList()
    {
        return new List<string>
        {
            "Tutorial",
            "Very Easy",
            "Easy",
            "Easier",
            "Basic",
            "Medium",
            "Hard",
            "Very Hard",
            "Nightmare"
        };
    }

    private static string GetMapDifficulty(int mapSize, List<string> difficultyList)
    {
        if (mapSize <= 10)
        {
            return difficultyList[0];
        }
        if (mapSize >= 11 && mapSize <= 20)
        {
            return difficultyList[1];
        }
        if (mapSize >= 21 && mapSize <= 25)
        {
            return difficultyList[2];
        }
        if (mapSize >= 26 && mapSize <= 30)
        {
            return difficultyList[3];
        }
        if (mapSize >= 31 && mapSize <= 50)
        {
            return difficultyList[4];
        }
        if (mapSize >= 51 && mapSize <= 80)
        {
            return difficultyList[5];
        }
        if (mapSize >= 81 && mapSize <= 120)
        {
            return difficultyList[6];
        }
        if (mapSize >= 121 && mapSize <= 150)
        {
            return difficultyList[7];
        }
        if (mapSize >= 151)
        {
            return difficultyList[8];
        }
        else
            return difficultyList[8];
    }

    private static string GetGateDifficulty(int gates, List<string> difficultyList)
    {
        switch (gates)
        {
            case 1:
                return difficultyList[0];
            case 2:
                return difficultyList[1];
            case 3:
                return difficultyList[2];
            case 4:
                return difficultyList[3];
            case 5:
                return difficultyList[4];
            case 6:
            case 7:
                return difficultyList[5];
            case 8:
            case 9:
            case 10:
                return difficultyList[6];
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
                return difficultyList[7];
            default:
                return difficultyList[8];
        }
    }

    private static string WorkOutDifficultyTotal(string map, string gate, List<string> difficultyList)
    {
        int mapIndex = difficultyList.IndexOf(map);
        int gateIndex = difficultyList.IndexOf(gate);
        int averageIndex;
        if ((mapIndex + gateIndex) % 2 == 1)
        {
            averageIndex = (mapIndex + gateIndex + 1) / 2;
        }
        else
        {
            averageIndex = (mapIndex + gateIndex) / 2;
        }
        return difficultyList[averageIndex];
    }
}

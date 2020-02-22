using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveGameData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.maze";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(LevelProgress.GetCompletedLevels());

        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();

        Debug.Log("Saved game");
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gamedata.maze";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            GameData gameData = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            Debug.Log("Loaded game");

            return gameData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

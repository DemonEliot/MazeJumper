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
        GameData gameData = new GameData(LevelManager.GetCompletedLevels());

        try
        {
            binaryFormatter.Serialize(fileStream, gameData);
        }
        catch
        {
        }
        finally
        {
            fileStream.Close();
        }
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gamedata.maze";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            try
            {
                GameData gameData = (GameData)binaryFormatter.Deserialize(fileStream);
                return gameData;
            }
            finally
            {
                fileStream.Close();
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

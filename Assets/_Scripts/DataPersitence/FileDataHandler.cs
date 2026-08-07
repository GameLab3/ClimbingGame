using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirectory = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirectory, string dataFileName)
    {
        this.dataDirectory = dataDirectory;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirectory, dataFileName);

        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when loading data from file: {fullPath}\n{e}");
            }
        }
        return loadedData;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dataDirectory, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            string dataToStore = JsonUtility.ToJson(gameData, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error when trying to save data to a file: {fullPath}\n{e}");
        }
    }
}

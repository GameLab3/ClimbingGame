using UnityEngine;
using System;
using System.Collections.Generic;
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

    public GameData Load(string profileId)
    {
        string fullPath = Path.Combine(dataDirectory, profileId, dataFileName);

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
    
    public SettingsData LoadSettings()
    {
        string fullPath = Path.Combine(dataDirectory, dataFileName);

        SettingsData loadedData = null;

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
                
                loadedData = JsonUtility.FromJson<SettingsData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when loading data from file: {fullPath}\n{e}");
            }
        }
        return loadedData;
    }

    public void Save(GameData gameData, string profileId)
    {
        string fullPath = Path.Combine(dataDirectory, profileId, dataFileName);

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
    
    public void SaveSettings(SettingsData settings)
    {
        string fullPath = Path.Combine(dataDirectory, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            string dataToStore = JsonUtility.ToJson(settings, true);

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

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        
        // Loop through all folders in the directory
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirectory).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            
            // This will check if we are adding the correct type of data to the dictionary
            string fullPath = Path.Combine(dataDirectory, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                continue;
            }
            
            // Load the data and add it to the dictionary
            GameData profileData = Load(profileId);

            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError($"Error when loading data from file: {fullPath}");
            }
            
        }
        
        return profileDictionary;
    }
}

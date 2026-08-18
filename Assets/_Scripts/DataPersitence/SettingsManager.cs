using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private string fileName;
    
    private SettingsData settingsData;
    
    private List<ISettingsPersistence> settingsPersistences;
    
    private FileDataHandler fileDataHandler;
    public static SettingsManager Instance {get; private set;}

    private bool loaded;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        settingsPersistences = FindAllSettingsPersistences();
        Load();
    }

    public void CreateNewSettings()
    {
        settingsData = new SettingsData();
    }

    public void ResetSettings()
    {
        settingsData = new SettingsData();
        settingsPersistences = FindAllSettingsPersistences();
        foreach (ISettingsPersistence settingsPersistence in settingsPersistences)
        {
            settingsPersistence.LoadSettingsData(settingsData);
        }
        fileDataHandler.SaveSettings(settingsData);
    }

    public void LoadSettings()
    {
        if (!loaded) return;
        settingsPersistences = FindAllSettingsPersistences();
        Load();
    }

    private void Load()
    {
        settingsData = fileDataHandler.LoadSettings();
        if (settingsData == null)
        {
            CreateNewSettings();
        }

        foreach (ISettingsPersistence settingsPersistences in settingsPersistences)
        {
            settingsPersistences.LoadSettingsData(settingsData);
        }
        
        if (!loaded) loaded = true;
    }

    public void SaveSettings()
    {
        settingsPersistences = FindAllSettingsPersistences();
        Save();
    }

    private void Save()
    {
        foreach (ISettingsPersistence settingsPersistences in settingsPersistences)
        {
            settingsPersistences.SaveSettingsData(ref settingsData);
        }
        
        fileDataHandler.SaveSettings(settingsData);
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
    
    private List<ISettingsPersistence> FindAllSettingsPersistences()
    {
        IEnumerable<ISettingsPersistence> dataPersistenceObjects = FindObjectsByType(typeof(MonoBehaviour), FindObjectsSortMode.None).OfType<ISettingsPersistence>();
        return new List<ISettingsPersistence>(dataPersistenceObjects);
    }
}

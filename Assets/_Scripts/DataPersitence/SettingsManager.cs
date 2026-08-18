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
        
    }

    public void CreateNewSettings()
    {
        settingsData = new SettingsData();
    }

    public void LoadSettings()
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
    }

    public void SaveSettings()
    {
        foreach (ISettingsPersistence settingsPersistences in settingsPersistences)
        {
            settingsPersistences.SaveSettingsData(ref settingsData);
        }
        
        fileDataHandler.SaveSettings(settingsData);
    }

    public void ChangePixelModeStatus(bool value)
    {
        
    }

    public void ChangePixelModeSetting(float value)
    {
        
    }
    
    
    
    
    
    private List<ISettingsPersistence> FindAllSettingsPersistences()
    {
        IEnumerable<ISettingsPersistence> dataPersistenceObjects = FindObjectsByType(typeof(MonoBehaviour), FindObjectsSortMode.None).OfType<ISettingsPersistence>();
        return new List<ISettingsPersistence>(dataPersistenceObjects);
    }
}

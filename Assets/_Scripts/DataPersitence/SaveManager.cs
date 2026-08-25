using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private string fileName;
    
    private GameData _gameData;
    
    private List<IDataPersistence> dataPersistenceObjects;
    
    private FileDataHandler dataHandler;
    
    private string selectedProfileId = "woop";
    public static SaveManager Instance {get; private set;}
    
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
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void ChangeSelectedProfileId(string profileId)
    {
        selectedProfileId = profileId;
        LoadGame();
    }
    
    public void CreateNewGameData()
    {
        _gameData = new GameData();
        //_gameData = new GameData
        //{
        //    respawnPoints = new S_RespawnPoint[S_RespawnManager.Instance.GetRespawnPointCount()]
        //};
        //var list = S_RespawnManager.Instance.GetRespawnList();
        //for (int i = 0; i < list.Count; i++)
        //{
        //    _gameData.respawnPoints[i] = list[i];
        //}
    }

    public void LoadGame()
    {
        _gameData = dataHandler.Load(selectedProfileId);
        
        if (_gameData == null)
        {
            Debug.Log("Data is null");
            CreateNewGameData();
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadGameData(_gameData);
        }
    }
    
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveGameData(ref _gameData);
        }
        
        dataHandler.Save(_gameData, selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType(typeof(MonoBehaviour), FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}

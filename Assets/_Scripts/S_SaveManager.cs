using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_SaveManager : MonoBehaviour
{
    private GameData _gameData;
    
    private List<IDataPersistence> dataPersistenceObjects;
    public static S_SaveManager Instance {get; private set;}
    
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
    }

    private void Start()
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGameData();
    }
    
    public void CreateNewGameData()
    {
        _gameData = new GameData
        {
            respawnPoints = new S_RespawnPoint[S_RespawnManager.Instance.GetRespawnPointCount()]
        };
        var list = S_RespawnManager.Instance.GetRespawnList();
        for (int i = 0; i < list.Count; i++)
        {
            _gameData.respawnPoints[i] = list[i];
        }
    }

    public void LoadGameData()
    {
        if (_gameData == null)
        {
            Debug.Log("Data is null");
            CreateNewGameData();
        }
    }
    
    public void SaveGameData()
    {
        
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType(typeof(MonoBehaviour), FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}

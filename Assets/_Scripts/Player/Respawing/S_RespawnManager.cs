using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using UnityEngine;

public class S_RespawnManager : MonoBehaviour, IDataPersistence
{
    public static S_RespawnManager Instance;
    
    [SerializeField] private int life = 3;
    
    [SerializeField] private List<S_RespawnPoint> checkPoints = new List<S_RespawnPoint>();
    
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


    public void ResetLife()
    {
        life = 3;
    }
    
    public void LoseLife()
    {
        life --;
        if (life <= 0)
        {
            // Trigger game over stuff
        }
    }

    public void LoadGameData(GameData gameData)
    {
        life = gameData.life;
        checkPoints.Clear();
        foreach (var item in gameData.respawnPoints)
        {
            checkPoints.Add(item);
        }
    }

    public void SaveGameData(ref GameData gameData)
    {
        gameData.life = life;
        for (int i = 0; i < checkPoints.Count; i++)
        {
            gameData.respawnPoints[i] = checkPoints[i];
        }
    }

    public int GetRespawnPointCount()
    {
        return checkPoints.Count;
    }

    public List<S_RespawnPoint> GetRespawnList()
    {
        return checkPoints;
    }

    public S_RespawnPoint[] GetRespawnArray()
    {
        return checkPoints.ToArray();
    }
    
    [Button]
    public S_RespawnPoint GetActiveRespawnPoint(int number = 0)
    {
        var newList = new List<S_RespawnPoint>();
        foreach (var item in checkPoints)
        {
            if (!item.activated) continue;
            newList.Add(item);
        }

        if (newList.Count != 0 && newList[newList.Count - 1 - number])
        {
            return newList[newList.Count - 1 - number];
        }
        return null;
    }

    public void MoveCheckPointToBottomOfList(S_RespawnPoint checkPoint)
    {
        var newList = checkPoints;
        if (newList.Contains(checkPoint))
        {
            newList.Remove(checkPoint);
            newList.Add(checkPoint);
        }
        checkPoints = newList;
    }
    
    public void AddCheckPointToList(S_RespawnPoint checkPoint)
    {
        if (checkPoints.Contains(checkPoint)) return;
        checkPoints.Add(checkPoint);
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        checkPoints = checkPoints.Where(x => x).ToList();
    }
    #endif
}

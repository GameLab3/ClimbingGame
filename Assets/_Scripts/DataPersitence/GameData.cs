using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public S_RespawnPoint[] respawnPoints;
    public SerializableDictionary<string, bool> respawnPointDict;
    public Vector3 playerPosition;
    public int life;

    public GameData()
    {
        respawnPointDict = new SerializableDictionary<string, bool>();
        playerPosition = Vector3.zero;
        respawnPoints = Array.Empty<S_RespawnPoint>();
        life = 3;
    }
}

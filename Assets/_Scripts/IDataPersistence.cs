using UnityEngine;

public interface IDataPersistence
{
    void LoadGameData(GameData gameData);
    void SaveGameData(ref GameData gameData);
}

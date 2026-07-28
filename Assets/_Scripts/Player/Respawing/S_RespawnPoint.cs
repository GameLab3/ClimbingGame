using EasyButtons;
using UnityEngine;

public class S_RespawnPoint : MonoBehaviour
{
    [HideInInspector]
    public bool activated;

    public Transform RespawnPoint()
    {
        return transform;
    }
    

    [Button]
    public void Activate()
    {
        activated = true;
        S_RespawnManager.Instance.MoveCheckPointToBottomOfList(this);
    }
    
    #if (UNITY_EDITOR)
    private void OnValidate()
    {
        FindFirstObjectByType<S_RespawnManager>().AddCheckPointToList(this);
    }
    #endif
}

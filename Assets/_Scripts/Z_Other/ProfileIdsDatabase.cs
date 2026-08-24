using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProfileIdsDatabase", menuName = "Tools/ProfileIdsDatabase")]
public class ProfileIdsDatabase : ScriptableObject
{
    public List<string> profileIds = new List<string>();
    
    private static ProfileIdsDatabase _instance;

    public static ProfileIdsDatabase Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<ProfileIdsDatabase>("ProfileIdsDatabase");
            }
            return _instance;
        }
    }

    public static int GetNumberOfProfileIds()
    {
        if (Instance) return Instance.profileIds.Count;
        return -1;
    }

    public static string GetProfileId(int index)
    {
        if (Instance && Instance.profileIds.Count > index && index >= 0)
        {
            return Instance.profileIds[index];
        }
        
        return "noProfileId";
    }
}

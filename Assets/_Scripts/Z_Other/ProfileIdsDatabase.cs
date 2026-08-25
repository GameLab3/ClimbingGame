using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProfileIdsDatabase", menuName = "Tools/ProfileIdsDatabase")]
public class ProfileIdsDatabase : ScriptableObject
{
    public List<string> profileIds = new List<string>();
    
    private static ProfileIdsDatabase _instance;

    private static ProfileIdsDatabase instance
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
        if (instance) return instance.profileIds.Count;
        return -1;
    }

    public static string GetProfileId(int index)
    {
        if (instance && instance.profileIds.Count > index && index >= 0)
        {
            return instance.profileIds[index];
        }
        
        return "noProfileId";
    }
}

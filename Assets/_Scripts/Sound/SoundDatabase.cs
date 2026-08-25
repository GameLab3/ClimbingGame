using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Tools/SoundDatabase")]
public class SoundDatabase : ScriptableObject
{
    public List<Sound> musicList = new List<Sound>();
    public List<Sound> sfxList = new List<Sound>();
    
    private static SoundDatabase _instance;

    public static SoundDatabase Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<SoundDatabase>("SoundDatabase");
            }
            return _instance;
        }
    }

    public static Sound GetSound(SoundID soundID, SoundType soundType)
    {
        if (Instance)
        {
            Sound sound = null;
            switch (soundType)
            {
                case SoundType.Music:
                    sound = Instance.LookThroughList(Instance.musicList, soundID);
                    break;
                case SoundType.Sfx:
                    sound = Instance.LookThroughList(Instance.sfxList, soundID);
                    break;
            }
            return sound;
        }
        
        return null;
    }

    private Sound LookThroughList(List<Sound> soundList, SoundID soundID)
    {
        Sound sound = soundList.Find(s => GetCleanName(s.name).Equals(soundID.ToString()));
        return sound;
    }
    
    
    private string GetCleanName(string name)
    {
        string clean = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-zA-Z0-9_]", "_");
        return char.IsDigit(clean[0]) ? "_" + clean : clean;
    }
}

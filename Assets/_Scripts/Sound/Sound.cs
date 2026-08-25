using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip[] audioClips;
    [Range(0f,1f)] public float volume;
    [Range(0f, 3f)] public float pitch;
    public bool loop;
    public bool playOnAwake;

    public static Sound CreateMusic(string musicName = "music name")
    {
        return new Sound
        {
            name = musicName,
            volume = 1f,
            pitch = 1f,
            loop = true,
            playOnAwake = false
        };
    }

    public static Sound CreateSound(string soundName = "sound name")
    {
        return new Sound
        {
            name = soundName,
            volume = 1f,
            pitch = 1f,
            loop = false,
            playOnAwake = false
        };
    }
}

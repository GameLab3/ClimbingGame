using UnityEngine;

public class SoundManager : MonoBehaviour, ISettingsPersistence
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(this);
    }

    public void LoadSettingsData(SettingsData settings)
    {
        bgmAudioSource.volume = settings.MusicVolume;
        sfxAudioSource.volume = settings.SFXVolume;
    }

    public void SaveSettingsData(ref SettingsData settings)
    {
        
    }
}

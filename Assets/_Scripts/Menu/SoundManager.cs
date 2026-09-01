using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour, ISettingsPersistence
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    
    private float _bgmVolume;
    private float _sfxVolume;
    
    private Coroutine _bgmCoroutine;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
            return;
        }
        
        DontDestroyOnLoad(this);
    }

    public void LoadSettingsData(SettingsData settings)
    {
        _bgmVolume = settings.MusicVolume;
        _sfxVolume = settings.SFXVolume;
    }

    public void SaveSettingsData(ref SettingsData settings)
    {
        
    }

    public void PlaySfx(SoundID soundID)
    {
        Sound sound = SoundDatabase.GetSound(soundID, SoundType.Sfx);
        if (sound == null) return;
        if (sound.audioClips.Length == 0) return;
        
        int index = Random.Range(0, sound.audioClips.Length);
        
        sfxAudioSource.PlayOneShot(sound.audioClips[index]);
    }

    public void PlayBGM(SoundID soundID, float fadeTime = 0.5f)
    {
        Sound sound = SoundDatabase.GetSound(soundID, SoundType.Music);
        if (sound == null) return;
        if (sound.audioClips.Length == 0) return;

        if (_bgmCoroutine != null)
        {
            StopCoroutine(_bgmCoroutine);
        }
        _bgmCoroutine = StartCoroutine(PlayMusicRoutine(sound, fadeTime));
    }

    private IEnumerator PlayMusicRoutine(Sound sound, float fadeTime)
    {
        yield return Fade(0, fadeTime);
        
        int index = Random.Range(0, sound.audioClips.Length);
        bgmAudioSource.clip = sound.audioClips[index];
        float volume = sound.volume * _bgmVolume;
        bgmAudioSource.volume = volume;
        bgmAudioSource.Play();
        
        yield return Fade(volume, fadeTime);
    }

    private IEnumerator Fade(float targetVolume, float fadeTime)
    {
        float startVolume = bgmAudioSource.volume;
        float timeElapsed = 0f;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            bgmAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, timeElapsed / fadeTime);
            yield return null;
        }
        
        bgmAudioSource.volume = targetVolume;
    }
}

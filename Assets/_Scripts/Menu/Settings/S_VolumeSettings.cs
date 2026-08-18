using UnityEngine;
using UnityEngine.UI;

public class S_VolumeSettings : MonoBehaviour, ISettingsPersistence
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    public void LoadSettingsData(SettingsData settings)
    {
        musicVolumeSlider.value = settings.MusicVolume;
        sfxVolumeSlider.value = settings.SFXVolume;
    }

    public void SaveSettingsData(ref SettingsData settings)
    {
        settings.MusicVolume = musicVolumeSlider.value;
        settings.SFXVolume = sfxVolumeSlider.value;
    }
}

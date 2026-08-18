using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float SFXVolume;
    public float MusicVolume;
    public bool PixelModeStatus;
    public int PixelModeValue;

    public SettingsData()
    {
        SFXVolume = 0.5f;
        MusicVolume = 0.5f;
        PixelModeStatus = false;
        PixelModeValue = 1;
    }
}

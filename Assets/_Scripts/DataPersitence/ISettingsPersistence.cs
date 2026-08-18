using UnityEngine;

public interface ISettingsPersistence
{
    void LoadSettingsData(SettingsData settings);
    void SaveSettingsData(ref SettingsData settings);
}

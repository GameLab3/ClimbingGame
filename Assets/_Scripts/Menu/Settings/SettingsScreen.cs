using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    void Start()
    {
        SettingsManager.Instance.LoadSettings();
    }

    public void SaveSettings()
    {
        SettingsManager.Instance.SaveSettings();
    }

    public void ResetSettings()
    {
        ButtonsInfo[] popupButtons = new ButtonsInfo[]
        {
            new ButtonsInfo("Yes", SettingsManager.Instance.ResetSettings),
            new ButtonsInfo("No")
        };
        
        S_PopupManager.Instance.ShowPopupScreen(new PopupSpecifics("Are you sure?", "This will reset all settings back to their default values.", popupButtons));
    }
}

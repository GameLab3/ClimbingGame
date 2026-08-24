using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileScreen : MonoBehaviour
{

    [SerializeField] private S_SaveSlot saveSlotPrefab;

    [SerializeField] private GameObject saveSlotHolder;
    
    private S_SaveSlot[] _saveSlots;

    private void Awake()
    {
        _saveSlots = new S_SaveSlot[ProfileIdsDatabase.GetNumberOfProfileIds()];
        for (int i = 0; i < ProfileIdsDatabase.GetNumberOfProfileIds(); i++)
        {
            string profileId = ProfileIdsDatabase.GetProfileId(i);
            S_SaveSlot saveSlot = Instantiate(saveSlotPrefab, saveSlotHolder.transform);
            saveSlot.Initialize(profileId);
            _saveSlots[i] = saveSlot;
        }
    }

    private void Start()
    {
        ActivateMenu();
    }

    public void ActivateMenu()
    {
        Dictionary<string, GameData> profilesGameData = SaveManager.Instance.GetAllProfilesGameData();

        foreach (S_SaveSlot saveSlot in _saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            
            saveSlot.OnClick += () => LoadProfile(saveSlot.GetProfileId());
        }
    }

    private void LoadProfile(string profileId = "noProfileId")
    {
        SaveManager.Instance.ChangeSelectedProfileId(profileId);
        
        ButtonsInfo[] buttons = new ButtonsInfo[]
        {
            new ButtonsInfo("Play"),
            new ButtonsInfo("Delete", () => ConfrontDeletion(profileId)),
            new ButtonsInfo("Back")
        };
        
        PopupManager.Instance.ShowPopupScreen(new PopupSpecifics(profileId, "This is how far you've gotten.", buttons));
    }

    private void ConfrontDeletion(string profileId)
    {
        ButtonsInfo[] buttons = new ButtonsInfo[]
        {
            new ButtonsInfo("Yes"),
            new ButtonsInfo("No", () => LoadProfile(profileId))
        };
        
        PopupManager.Instance.ShowPopupScreen(new PopupSpecifics("Are you sure?", "This will cause the savefile to be permanently deleted.", buttons));
    }
}

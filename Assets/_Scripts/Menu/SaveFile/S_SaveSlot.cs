using System;
using UnityEngine;
using UnityEngine.UI;

public class S_SaveSlot : MonoBehaviour
{
    [Header("Profile Id")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;

    public event Action OnClick;
    
    public void Initialize(string profileId)
    {
        this.profileId = profileId;
    }
    
    public void SetData(GameData gameData)
    {
        if (gameData == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
        }
    }

    public string GetProfileId()
    {
        return profileId;
    }

    public void Clicked()
    {
        OnClick?.Invoke();
    }
}

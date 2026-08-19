using UnityEngine;

public class SaveFileScreen : MonoBehaviour
{
    public void LoadProfile(string profileId = "noProfileId")
    {
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

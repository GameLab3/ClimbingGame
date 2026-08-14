using System;
using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

public class S_InitializeAndTestPopup : MonoBehaviour
{
    [SerializeField] private S_PopupScreen popupScreenPrefab;
    
    private S_PopupScreen _createdPopupScreen;
    
    [SerializeField] private PopupSpecifics popupSpecifics;

    [Button]
    public void InitializePopupScreen()
    {
        if (_createdPopupScreen) return;
        
        _createdPopupScreen = Instantiate(popupScreenPrefab);
        _createdPopupScreen.InitializePopup(popupSpecifics);
        
        for (int i = 0; i < popupSpecifics.buttons.Length; i++)
        {
            var index = i;
            _createdPopupScreen.GetButton(i).OnClick += () => TriggerEvent(index);
        }
    }

    private void TriggerEvent(int index)
    {
        popupSpecifics.buttons[index].buttonEvent?.Invoke();
    }

    public void DestroyPopupScreen()
    {
        if (_createdPopupScreen)
        {
            Destroy(_createdPopupScreen.gameObject);
        }
    }

    public void DisplayTextInConsole(string text)
    {
        Debug.Log(text);
    }
}

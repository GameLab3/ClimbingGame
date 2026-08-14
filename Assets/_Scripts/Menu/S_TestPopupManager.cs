using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

public class S_TestPopupManager : MonoBehaviour
{
    [SerializeField] private PopupSpecifics popupSpecifics;

    [Button]
    public void TestPopupOn()
    {
        S_PopupManager.Instance.ShowPopupScreen(popupSpecifics);
    }

    [Button]
    public void AnotherTest()
    {
        S_PopupManager.Instance.ShowTextOnlyPopupScreen(popupSpecifics);
    }

    public void ShowTextInConsole(string text)
    {
        Debug.Log(text);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class S_PopupManager : MonoBehaviour
{
    public static S_PopupManager Instance;

    [SerializeField] private S_PopupScreen popupScreenPrefab;
    [SerializeField] private S_PopupScreen textOnlyPopupScreenPrefab;
    
    private S_PopupScreen _popupScreen;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// Creates the popup screen prefab using the popup specifics. Will also connect all unity events set in the popup specifics.
    /// </summary>
    /// <param name="popupSpecifics"></param>
    public void ShowPopupScreen(PopupSpecifics popupSpecifics)
    {
        if (!popupScreenPrefab) return;
        if (popupSpecifics == null) return;
        if (_popupScreen) HidePopupScreen();
        
        
        _popupScreen = Instantiate(popupScreenPrefab, transform);
        _popupScreen.InitializePopup(popupSpecifics);
        
        for (int i = 0; i < popupSpecifics.buttons.Length; i++)
        {
            var index = i;
            _popupScreen.GetButton(i).OnClick += HidePopupScreen;
            _popupScreen.GetButton(i).OnClick += () => popupSpecifics.buttons[index].buttonEvent?.Invoke();
        }
    }

    /// <summary>
    /// Creates the text only popup screen prefab using the popup specifics. Only creates a maximum of one button. It will also connect all unity events set to the first button in the popup specifics.
    /// </summary>
    /// <param name="popupSpecifics"></param>
    public void ShowTextOnlyPopupScreen(PopupSpecifics popupSpecifics)
    {
        if (!textOnlyPopupScreenPrefab) return;
        if (popupSpecifics == null) return;
        if (_popupScreen) HidePopupScreen();
        
        ButtonsInfo[] buttonsInfo = new ButtonsInfo[1];
        
        buttonsInfo[0] = popupSpecifics.buttons.Length > 0 ? popupSpecifics.buttons[0] : new ButtonsInfo("", new UnityEvent());
        
        var newPopupSpecifics = new PopupSpecifics(popupSpecifics.title, popupSpecifics.description, buttonsInfo);
        
        _popupScreen = Instantiate(textOnlyPopupScreenPrefab, transform);
        _popupScreen.InitializePopup(newPopupSpecifics);

        _popupScreen.GetButton(0).OnClick += HidePopupScreen;
        if (popupSpecifics.buttons.Length <= 0) return;
        _popupScreen.GetButton(0).OnClick += () => popupSpecifics.buttons[0].buttonEvent?.Invoke();
    }

    private void HidePopupScreen()
    {
        if (_popupScreen)
        {
            Destroy(_popupScreen.gameObject);
        }
    }
}

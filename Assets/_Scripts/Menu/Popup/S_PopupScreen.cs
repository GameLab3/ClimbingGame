using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class S_PopupScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject buttonHolder;
    [SerializeField] private S_PopupButton buttonPrefab;
    [SerializeField] private S_PopupButton[] buttons;

    public void InitializePopup(PopupSpecifics specifics)
    {
        buttons = new S_PopupButton[specifics.buttons.Length];

        titleText.text = specifics.title;
        descriptionText.text = specifics.description;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, buttonHolder.transform);
            buttons[i].InitializeButton(specifics.buttons[i].buttonText);
        }
    }

    public S_PopupButton GetButton(int i)
    {
        return i >= buttons.Length ? null : buttons[i];
    }
}

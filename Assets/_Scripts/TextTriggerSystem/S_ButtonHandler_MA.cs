using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_ButtonHandler_MA : MonoBehaviour
{
    public Button button { get; private set; }
    [SerializeField] TMP_Text text;
    ButtonSpecifics buttonSpecifics;
    public void InitializeButton(ButtonSpecifics specifics)
    {
        buttonSpecifics = specifics;
        button = GetComponent<Button>();
        UpdateButton();
    }

    void UpdateButton()
    {
        text.text = buttonSpecifics.Answer;
    }
}

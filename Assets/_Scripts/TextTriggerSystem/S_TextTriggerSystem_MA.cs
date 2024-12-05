using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_TextTriggerSystem_MA : MonoBehaviour
{
    [SerializeField] List<Dialogue> dialogueText = new();
    int displayNumber;

    [SerializeField] TMP_Text textBox;
    [SerializeField] GameObject dialogueParent;
    bool textCanProgress;
    bool hasHappened;

    [SerializeField] bool hasQuestions;
    [SerializeField] S_ButtonHandler_MA buttonPrefab;
    [SerializeField] Transform buttonsParent;

    S_IncorrectAnswer_MA S_IncorrectAnswer_MA;
    S_DialogueColorSwitcher_MA S_DialogueColorSwitcher_MA;
    S_WinScene_MA S_WinScene_MA;

    int triggerInt;
    [SerializeField] Transform parent;
    BoxCollider nextTrigger;

    private void Start()
    {
        S_IncorrectAnswer_MA = FindFirstObjectByType<S_IncorrectAnswer_MA>();
        S_DialogueColorSwitcher_MA = FindFirstObjectByType<S_DialogueColorSwitcher_MA>();
        S_WinScene_MA = FindFirstObjectByType<S_WinScene_MA>();

        if (S_DialogueColorSwitcher_MA == null) return;

        triggerInt = transform.GetSiblingIndex() +1;
        if (parent.transform.childCount <= triggerInt) { return; }

        nextTrigger = parent.transform.GetChild(triggerInt).GetComponent<BoxCollider>();
    }

    bool isAtQuestion;

    void SetActive()
    {
        if (S_DialogueColorSwitcher_MA != null) { S_DialogueColorSwitcher_MA.ChangeCameraColors(); }
        dialogueParent.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        textCanProgress = true;

        if (!hasHappened)
        {
            SetActive();
            DisplayText();
            hasHappened = true;
        }
    }

    private void OnAttack()
    {
        if (!isAtQuestion)
        {
            DisplayText();
        }
    }

    void DisplayText()
    {
        if (textCanProgress && dialogueText.Count > displayNumber)
        {
            textBox.text = dialogueText[displayNumber].Text;

            if (dialogueText[displayNumber].isQuestion)
            {
                AskQuestion();
                isAtQuestion = true;
            }

            displayNumber++;
        }
        else if (textCanProgress && dialogueText.Count >= displayNumber)
        {
            dialogueParent.SetActive(false);
            if (nextTrigger != null)
            {
                nextTrigger.isTrigger = true;
            }
            S_DialogueColorSwitcher_MA.ChangeCameraColors();
            Destroy(gameObject);
        }
    }
    void AskQuestion()
    {
        for (int i = 0; i < dialogueText[displayNumber].options.Length; i++)
        {
            dialogueText[displayNumber].options[i].assosiatedButton = Instantiate(buttonPrefab, buttonsParent);
            dialogueText[displayNumber].options[i].assosiatedButton.InitializeButton(dialogueText[displayNumber].options[i]);

            if (dialogueText[displayNumber].options[i].isCorrect)
            {
                dialogueText[displayNumber].options[i].assosiatedButton.button.onClick.AddListener(CorrectOption);
            }
            else if (dialogueText[displayNumber].options[i].isWin)
            {
                dialogueText[displayNumber].options[i].assosiatedButton.button.onClick.AddListener(WinOption);
            }
            else
            {
                dialogueText[displayNumber].options[i].assosiatedButton.button.onClick.AddListener(WrongOption);
            }
        }

    }

    public void CorrectOption()
    {
        isAtQuestion = false;

        foreach (Transform item in buttonsParent)
        {
            Destroy(item.gameObject);
        }

        DisplayText();  
    }
    public void WrongOption()
    {
        foreach (Transform item in buttonsParent)
        {
            Destroy(item.gameObject);
        }
        S_IncorrectAnswer_MA.InstantiateEnd();
    }

    public void WinOption()
    {
        foreach(Transform item in buttonsParent)
        {
            Destroy(item.gameObject);
        }
        S_WinScene_MA.InstantiateWin();
    }
}

[Serializable]
public class ButtonSpecifics
{
    public string Answer;
    public S_ButtonHandler_MA assosiatedButton;
    public bool isCorrect;
    public bool isWin;
}
[Serializable]
public class Dialogue
{
    public string Text;
    public bool isQuestion;
    public ButtonSpecifics[] options;
}

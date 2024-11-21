using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_TextTriggerSystem_MA : MonoBehaviour
{
    [SerializeField] List<string> dialogueText = new List<string>();
    int displayNumber;

    [SerializeField] TMP_Text textBox;
    [SerializeField] GameObject dialogueParent;
    bool textCanProgress;
    bool hasHappened;

    [SerializeField] bool hasQuestions;
    [SerializeField] GameObject buttons;

    void SetActive()
    {
        dialogueParent.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        textCanProgress = true;
        
        if(!hasHappened)
        {
            SetActive();
            DisplayText();
            hasHappened = true;
        }
    }

    private void OnAttack()
    {
        DisplayText();
    }

    void DisplayText()
    {
        if (textCanProgress && dialogueText.Count > displayNumber)
        {
            textBox.text = dialogueText[displayNumber];

            displayNumber++;
        }
        else if (textCanProgress && dialogueText.Count >= displayNumber)
        {
            CheckIfDone();
            Destroy(gameObject);
        }
    } 
        void CheckIfDone()
        {
            if (hasQuestions) { buttons.SetActive(true); }
            else { buttons.SetActive(false); dialogueParent.SetActive(false); }
        }
}

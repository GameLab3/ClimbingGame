using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_TextTriggerSystem_MA : MonoBehaviour
{
    [SerializeField] List<string> dialogueText = new List<string>();
    int displayNumber;

    TMP_Text textBox;
    bool textCanProgress;
    bool hasHappened;

    void Start()
    {
        textBox = FindFirstObjectByType<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        textCanProgress = true;
        
        if(!hasHappened)
        {
            textBox.gameObject.SetActive(true);
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
            textBox.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

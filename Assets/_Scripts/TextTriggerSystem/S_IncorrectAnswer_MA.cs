using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_IncorrectAnswer_MA : MonoBehaviour
{
    [SerializeField] List<string> endDialogue = new List<string>();
    int displayNumber;

    [SerializeField] TMP_Text textBox;
    bool textCanProgress;
    bool hasHappened;

    [SerializeField] string endScene;

    private void OnAttack()
    {
        DisplayText();
    }

    void DisplayText()
    {
        if (textCanProgress && endDialogue.Count > displayNumber)
        {
            textBox.text = endDialogue[displayNumber];

            displayNumber++;
        }
        else if (textCanProgress && endDialogue.Count >= displayNumber)
        {
            SceneManager.LoadScene(endScene);
        }
    }

    public void InstantiateEnd()
    {
        textCanProgress = true;

        if (!hasHappened)
        {
            DisplayText();
            hasHappened = true;
        }
    }
}

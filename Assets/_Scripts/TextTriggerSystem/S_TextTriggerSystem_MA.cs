using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_TextTriggerSystem_MA : MonoBehaviour
{
    [SerializeField] List<string> dialogueText = new List<string>();
    int textsInList;

    [SerializeField] TMP_Text textBox;
    

    private void OnAttack()
    {
        textBox.text = dialogueText[textsInList];

        if(textsInList >= dialogueText.Count)
        {
            Destroy(gameObject);
        }
        textsInList++;
    }
}

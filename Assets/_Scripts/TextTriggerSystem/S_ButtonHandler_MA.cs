using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ButtonHandler_MA : MonoBehaviour
{
    [SerializeField] GameObject buttons;

    S_IncorrectAnswer_MA S_IncorrectAnswer_MA;

    private void Start()
    {
        S_IncorrectAnswer_MA = GetComponent<S_IncorrectAnswer_MA>();
    }

    public void CorrectOption()
    {
        buttons.SetActive(false);
    }

    public void WrongOption()
    {
        buttons.SetActive(false);
        S_IncorrectAnswer_MA.InstantiateEnd();
    }
}

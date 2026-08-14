using System;
using TMPro;
using UnityEngine;

public class S_PopupButton : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public event Action OnClick;

    public void Clicked()
    {
        OnClick?.Invoke();
    }

    public void InitializeButton(string buttonText)
    {
        text.text = buttonText;
    }
}

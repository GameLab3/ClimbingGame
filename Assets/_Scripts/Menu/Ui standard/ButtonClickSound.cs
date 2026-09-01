using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (_button) _button.onClick.AddListener(PlayButtonSound);
    }

    private void OnDisable()
    {
        if (_button) _button.onClick.RemoveListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        SoundManager.Instance.PlaySfx(SoundID.Button_Click);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_PixelModeSetting : MonoBehaviour
{
    [SerializeField] private Toggle pixelModeToggle;
    [SerializeField] private Slider pixelModeSlider;
    [SerializeField] private TextMeshProUGUI pixelModeValueText;

    private void OnEnable()
    {
        pixelModeToggle.onValueChanged.AddListener(ToggleChanged);
        pixelModeSlider.onValueChanged.AddListener(SliderChanged);
    }

    private void OnDisable()
    {
        pixelModeToggle.onValueChanged.RemoveListener(ToggleChanged);
        pixelModeSlider.onValueChanged.RemoveListener(SliderChanged);
    }

    private void Start()
    {
        ToggleChanged(pixelModeToggle.isOn);
        SliderChanged(pixelModeSlider.value);
    }

    private void ToggleChanged(bool value)
    {
        pixelModeSlider.interactable = value;
        SettingsManager.Instance.ChangePixelModeStatus(value);
    }

    private void SliderChanged(float value)
    {
        pixelModeValueText.text = value.ToString();
        SettingsManager.Instance.ChangePixelModeSetting(value);
    }
}

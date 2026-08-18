using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderColour : MonoBehaviour
{
    [SerializeField] Gradient sliderGradient;
    private Slider _slider;
    private Image _imageFill;
    private Image _imageBackground;

    private float _minValue;
    private float _maxValue;

    private void Awake()
    {
        if(!_slider) _slider = GetComponent<Slider>();
        if(!_imageFill) _imageFill = _slider.fillRect.GetComponent<Image>();
        if(!_imageBackground) _imageBackground = transform.GetChild(0).GetComponent<Image>();
        _minValue = _slider.minValue;
        _maxValue = _slider.maxValue;
    }

    private void Start()
    {
        UpdateColour(_slider.value);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(UpdateColour);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(UpdateColour);
    }

    private void UpdateColour(float value)
    {
        _imageFill.color = GetGradientColour(value);
        _imageBackground.color = GetGradientColour(value);
    }

    private Color GetGradientColour(float value)
    {
        if (value <= _minValue)
        {
            return sliderGradient.Evaluate(0);
        }
        
        if (value >= _maxValue)
        {
            return sliderGradient.Evaluate(1);
        }
        
        var Value = value - _minValue;
        var MaxValue = _maxValue - _minValue;
        
        var p = Value / MaxValue;
        
        return sliderGradient.Evaluate(p);
    }
}

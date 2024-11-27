using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class S_ChangeSaturation_IS : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float playerStartY;
    [SerializeField] private float playerEndY;
    private float _cameraOffset;
    
    
    [Header("Saturation Settings")]
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private float minSaturationValue = -100f;
    [SerializeField] private float maxSaturationValue = 100f;
    [SerializeField] private float saturationValue = -100f;
    [SerializeField] private float saturationMultiplier = 0.014f;
    private ColorAdjustments _colorAdjustments;
    
    private void Start()
    {
        if (postProcessVolume == null)
        {
            postProcessVolume = FindFirstObjectByType<Volume>();
        }
        postProcessVolume.profile.TryGet(out _colorAdjustments);
        Invoke(nameof(SetCameraOffset), 1f);
    }

    private void SetCameraOffset()
    {
        _cameraOffset = playerStartY + transform.position.y;
    }

    private void ChangeSaturation()
    {
        var playerY = transform.position.y - _cameraOffset;
        saturationValue = playerY*playerY*saturationMultiplier -100f;
        saturationValue = Mathf.Clamp(saturationValue, minSaturationValue, maxSaturationValue);
        _colorAdjustments.saturation.value = saturationValue;
    }

    private void Update()
    {
        ChangeSaturation();
    }
    
    public void ChangeMinSaturation(float value)
    {
        minSaturationValue = value;
    }
}

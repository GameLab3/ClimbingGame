using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class S_DialogueColorSwitcher_MA : MonoBehaviour
{
    Volume volume;
    Vignette vignette;
    ShadowsMidtonesHighlights shadowsMidtonesHighlights;
    ChromaticAberration chromaticAberration;
    ColorCurves colorCurves;
    PaniniProjection paniniProjection;

    bool shouldBeActive;

    private void Start()
    {
        volume = FindFirstObjectByType<Volume>();

        if (volume == null) return;

        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out shadowsMidtonesHighlights);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out colorCurves);
        volume.profile.TryGet(out paniniProjection);
    }

    public void ChangeCameraColors()
    {
        if (volume == null) return;
        shouldBeActive = !shouldBeActive;

        vignette.active = shouldBeActive;
        shadowsMidtonesHighlights.active = shouldBeActive;
        chromaticAberration.active = shouldBeActive;
        colorCurves.active = shouldBeActive;
        paniniProjection.active = shouldBeActive;
    }

}

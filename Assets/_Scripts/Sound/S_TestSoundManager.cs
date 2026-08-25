using EasyButtons;
using UnityEngine;

public class S_TestSoundManager : MonoBehaviour
{
    [Button]
    public void PlayMusic(SoundID soundID, float fadeTime = 0.5f)
    {
        SoundManager.Instance.PlayBGM(soundID, fadeTime);
    }

    [Button]
    public void PlaySfx(SoundID soundID)
    {
        SoundManager.Instance.PlaySfx(soundID);
    }
}

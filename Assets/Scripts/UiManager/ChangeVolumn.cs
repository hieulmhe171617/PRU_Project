using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider musicSound;
    public Slider effectSound;
    private void Start()
    {
        if (musicSound)
        {
            musicSound.value = PlayerPrefs.GetFloat("musicSound");
        }
        if (effectSound)
        {
            effectSound.value = PlayerPrefs.GetFloat("effectSound");
        }
    }
    void Update()
    {
        if (musicSound)
        {
            SoundManager.Ins.SetVolumeMusic(musicSound.value);
        }
        if (effectSound)
        {
            SoundManager.Ins.SetVolumeSource(effectSound.value);
        }
    }
}

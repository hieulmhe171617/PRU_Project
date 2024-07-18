using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public static SoundManager instance {  get; private set; }
    public AudioSource source;

    public AudioSource musicSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } //destroy duplicated gameobject
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetFloat("musicSound", PlayerPrefs.GetFloat("musicSound"));
        PlayerPrefs.SetFloat("effectSound", PlayerPrefs.GetFloat("effectSound"));
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
    private void Update()
    {

    }
    public void SetVolumeSource(float value)
    {
        source.volume = value;
        PlayerPrefs.SetFloat("effectSound", value);
    }
    public void SetVolumeMusic(float value)
    {
        musicSound.volume = value;
        PlayerPrefs.SetFloat("musicSound", value);
    }

}

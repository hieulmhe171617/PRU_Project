using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public static SoundManager instance {  get; private set; }
    private AudioSource source;

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
    }
    public void SetVolumeMusic(float value)
    {
        musicSound.volume = value;
    }
}

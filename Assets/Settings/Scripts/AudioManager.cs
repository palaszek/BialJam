using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Background Music")]
    public AudioClip background;
    [Header("Sound Effects")]
    public AudioClip Chodzenie;
    public AudioClip Jedzenie;
    public AudioClip Otwieranie;
    public AudioClip U¿ycieItem;



    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}

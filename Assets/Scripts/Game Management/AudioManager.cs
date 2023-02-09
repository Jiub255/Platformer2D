using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource effectsSource;
    [SerializeField]
    private AudioSource musicSource;

    public void PlaySoundEffect(AudioClip effectClip)
    {
        if (effectsSource != null)
        {
            // Use PlayOneShot so I can play multiple clips at once without cutting off the previous played one. 
            effectsSource.PlayOneShot(effectClip);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }

    public void PauseMusic(AudioClip noReason)
    {
        musicSource.Pause();
    }
}
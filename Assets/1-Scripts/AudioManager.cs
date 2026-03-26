using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameAudios gameAudios;

    public AudioSource audioSource;


    public void PlayAudio(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }


}

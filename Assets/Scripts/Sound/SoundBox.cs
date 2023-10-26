using UnityEngine;

public enum SoundType
{
    DeadEasy,
    DeadMiddle,
    Shoot,
    TakeItem,
    Damage,
    Step,
    Jump,
    LevelSound
}
public class SoundBox : MonoBehaviour
{
    public static SoundBox Instance;

    public AudioClip[] SoundClips;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundType)
    {
        AudioClip soundClip = GetSoundClip(soundType);
        if (soundClip != null)
        {
            _audioSource.PlayOneShot(soundClip);
        }
    }

    private AudioClip GetSoundClip(SoundType soundType)
    {
        int soundIndex = (int)soundType;
        if (soundIndex >= 0 && soundIndex < SoundClips.Length)
        {
            return SoundClips[soundIndex];
        }
        return null;
    }
}

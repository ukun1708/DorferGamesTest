using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public AudioClip[] clips;
    public AudioSource audioSource;

    public void PlaySound(AudioType audioType, float volume, float pitch, bool loop)
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.PlayOneShot(clips[(int)audioType]);
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
    public enum AudioType
    {
        step,
        hit,
        destroy,
        loot,
        coin
    }
}

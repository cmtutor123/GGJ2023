using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> sounds = new List<AudioClip>();

    public AudioSource audioSource;

    public bool loopSound;
    public float loopDelay;
    private float loopCounter;
    public bool randomDelay;

    private bool canPlay = true;

    public void PlaySound()
    {
        if (canPlay)
        {
            audioSource.clip = sounds[Random.Range(0, sounds.Count - 1)];
            audioSource.Play();
        }
    }

    public void SetPlayable(bool playable)
    {
        canPlay = playable;
    }

    void Update()
    {
        loopCounter -= Time.deltaTime;
        if (loopSound && loopCounter <= 0)
        {
            loopCounter = loopDelay;
            if (randomDelay) loopCounter += Random.Range(0, 2);
            PlaySound();
        }
    }
}

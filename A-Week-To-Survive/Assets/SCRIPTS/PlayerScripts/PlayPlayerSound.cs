using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlayerSound : MonoBehaviour
{
    public AudioClip[] playerHurtSources;
    public AudioClip playerDeadSound;
    public AudioClip playerThinkingSound;
    public AudioSource playSpecificSound;

    public void PlayPlayerHurtSound()
    {
        playSpecificSound.clip = playerHurtSources[Random.Range(0, playerHurtSources.Length)];
        playSpecificSound.Play();   
    }

    public void PlayPlayerDeathSound()
    {
        playSpecificSound.clip = playerDeadSound;
        playSpecificSound.Play();
    }

    public void PlayThinkingSound()
    {
        playSpecificSound.clip = playerThinkingSound;
        playSpecificSound.Play();   
    }
}

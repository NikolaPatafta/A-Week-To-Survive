using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlayerSound : MonoBehaviour
{
    public AudioSource[] playerAudioSources;
    public AudioSource playerDeathSound;

    public void PlayPlayerHurtSound()
    {
        int i = 0;
        i = Random.Range(0, playerAudioSources.Length);
        playerAudioSources[i].Play();
    }

    public void PlayPlayerDeathSound()
    {
        playerDeathSound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;
    [SerializeField] private AudioClip doorHit;
    [SerializeField] private AudioClip doorBreak;
    [SerializeField] private AudioSource doorSource;

    public void PlayAudioDoorOpen()
    {
        doorSource.clip = doorOpen;
        doorSource.Play();  
    }

    public void PlayAudioDoorClose()
    {
        doorSource.clip = doorClose;
        doorSource.Play();
    }

    public void PlayAudioDoorHit()
    {
        doorSource.clip = doorHit;
        doorSource.Play();
    }

    public void PlayAudioDoorBreak()
    {
        doorSource.clip = doorBreak; 
        doorSource.Play();
    }
}

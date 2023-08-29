using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;
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
}

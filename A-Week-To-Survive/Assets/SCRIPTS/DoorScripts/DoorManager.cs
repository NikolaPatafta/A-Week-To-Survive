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

    public void PlayAudioDoor()
    {
        // Find the nearest AudioSource with 'DoorController' script
        AudioSource nearestDoorAudioSource = FindNearestDoorAudioSource();

        if (nearestDoorAudioSource != null)
        {
            // Play the 'doorBreak' AudioClip on the nearest AudioSource
            Debug.Log("Playing doorbreak");
            nearestDoorAudioSource.clip = doorBreak;
            nearestDoorAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource with 'DoorController' script found.");
        }
    }

    private AudioSource FindNearestDoorAudioSource()
    {
        DoorController[] doorControllers = FindObjectsOfType<DoorController>();
        PlayerPickup player = FindAnyObjectByType<PlayerPickup>();
        AudioSource nearestAudioSource = null;
        float minDistance = float.MaxValue;

        foreach (DoorController doorController in doorControllers)
        {
            AudioSource audioSource = doorController.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                float distance = Vector3.Distance(player.transform.position, doorController.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestAudioSource = audioSource;
                }
            }
        }

        return nearestAudioSource;
    }
}

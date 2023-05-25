using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    [SerializeField]
    private AudioClip doorOpen;
    [SerializeField]
    private AudioClip doorClose;
    [SerializeField]
    private AudioSource doorSource;

    private bool isDoorOpen = false;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void PlayDoorAnimation()
    {
        if (!isDoorOpen)
        {
            doorAnimator.Play("DoorOpen");
            doorSource.clip = doorOpen;
            doorSource.Play();
            isDoorOpen = true;
        }
        else
        {
            doorAnimator.Play("DoorClose");
            doorSource.clip = doorClose;
            doorSource.Play();
            isDoorOpen = false;
        }
    }

}

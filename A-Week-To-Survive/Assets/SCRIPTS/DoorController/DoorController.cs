using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;

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
            isDoorOpen = true;
        }
        else
        {
            doorAnimator.Play("DoorClose");
            isDoorOpen = false;
        }
    }

}

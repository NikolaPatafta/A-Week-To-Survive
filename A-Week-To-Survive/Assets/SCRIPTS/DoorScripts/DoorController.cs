using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform doorPrefab;
    private DoorManager doorManager;
    public float openRot = 90;
    public float closeRot = 0f;
    public float speed = 5f;
    public bool opening;
    public bool reversed;
    public bool specialDoor = false;


    private void Awake()
    {
        doorPrefab = GetComponent<Transform>();
        doorManager = FindAnyObjectByType<DoorManager>();
    }

    private void FixedUpdate()
    {
        Quaternion targetRotQuaternion;

        if (opening)
        {
            targetRotQuaternion = Quaternion.Euler(0, reversed ? openRot : -openRot, 0);
        }
        else
        {
            targetRotQuaternion = Quaternion.Euler(0, reversed ? closeRot : -closeRot, 0);
        }

        doorPrefab.localRotation = Quaternion.Lerp(doorPrefab.localRotation, targetRotQuaternion, speed * Time.deltaTime);
    }

    public void ToggleDoor()
    {
        opening = !opening; 
        if (opening)
        {
            doorManager.PlayAudioDoorOpen();
        }
        else
        {
            doorManager.PlayAudioDoorClose();
        }
    }

}

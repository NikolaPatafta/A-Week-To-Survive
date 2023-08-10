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


    private void Awake()
    {
        doorPrefab = GetComponent<Transform>();
        doorManager = FindAnyObjectByType<DoorManager>();
    }

    private void FixedUpdate()
    {
        Vector3 currentRot = doorPrefab.transform.localEulerAngles;
        if (opening)
        {
            if(currentRot.y < openRot)
            {
                doorPrefab.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);
                
            }
        }
        else
        {
            if(currentRot.y > closeRot)
            {
                doorPrefab.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
            }

        }
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

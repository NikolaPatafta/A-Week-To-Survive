using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBlockerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject barricade;
    [SerializeField] private GameObject timeline;
    [SerializeField] AudioManager audioManager;
    private DoorController currentDoor;
    private BoxCollider Boxcollider;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CloseDoors();
            StartCoroutine(StartShortCutScene());
        }
    }

    private void CloseDoors()
    {
        foreach(GameObject door in doors)
        {
            if (door != null)
            {
                DoorController controller = door.GetComponent<DoorController>();
                BoxCollider collider = door.GetComponentInChildren<BoxCollider>();
                if(controller != null)
                {
                    currentDoor = controller;
                    Boxcollider = collider;
                    currentDoor.opening = false;
                    Boxcollider.enabled= false;

                    Debug.Log("Closing door: " + controller);
                }
                else
                {
                    Debug.LogWarning("Controller is null!");
                }
            }
            else
            {
                Debug.LogWarning("door is null!");
            }
        }
    }
    private IEnumerator StartShortCutScene()
    {
        audioManager.canPlayMusic = false;
        audioManager.StopDayAudio();
        audioManager.StopNightAudio();  
        yield return new WaitForSeconds(0.5f);
        barricade.SetActive(true);
        timeline.SetActive(true);   
    }
}

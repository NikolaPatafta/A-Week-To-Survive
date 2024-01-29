using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBlockerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject barricade;
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject explosionTimeline;
    [SerializeField] AudioManager audioManager;
    [SerializeField] EnemyManager enemyManager;
    private DoorController currentDoor;
    private BoxCollider Boxcollider;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CloseDoors();
            StartCoroutine(StartShortCutScene());
            DisableZombies();
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

    private void DisableZombies()
    {
        EnemyController[] zombies = FindObjectsOfType<EnemyController>();

        foreach (EnemyController zombie in zombies)
        {
            if (zombie != null)
            {
                zombie.HardStop();
                zombie.enabled = false;
                enemyManager.StopSpawning();
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
        explosionTimeline.SetActive(true);
        gameObject.SetActive(false);
    }
}

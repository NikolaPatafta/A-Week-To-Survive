using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private DoorManager doorManager;
    public int health = 100;

    private void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();
    }

    public void DamageObstacle()
    {
        health = health - 10;
        doorManager.PlayAudioDoor();
        if(health <= 0)
        {
            doorManager.PlayAudioDoorBreak();
            Destroy(gameObject);
        }
    }


}

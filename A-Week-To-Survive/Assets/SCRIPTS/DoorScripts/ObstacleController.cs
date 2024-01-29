using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private DoorManager doorManager;
    public int health;
    public int maxhealth;

    private void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();
        maxhealth = health;
    }

    public void DamageObstacle()
    {
        health = health - 10;
        doorManager.PlayAudioDoor();
        UpdateDoorHealth doorHealth = FindObjectOfType<UpdateDoorHealth>();
        if (doorHealth != null)
        {
            doorHealth.UpdateHealth(health, maxhealth);  
        }
        if(health <= 0)
        {
            doorManager.PlayAudioDoorBreak();
            Destroy(gameObject);
        }
    }


}

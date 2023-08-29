using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : MonoBehaviour
{
    public float health = 200f;


    public void DamageDoor(float damage)
    {
        if(health <= 0)
        {
            DestroyDoor();
        }
        else
        {
            health -= damage;
        }   
    }

    public void DestroyDoor()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

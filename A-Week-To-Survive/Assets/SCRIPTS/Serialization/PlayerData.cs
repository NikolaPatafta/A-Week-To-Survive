using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] position;

    public PlayerData (HealthScript playerhealth)
    {
        health = playerhealth.health;
        
        position = new float[3];
        position[0] = playerhealth.transform.position.x;
        position[1] = playerhealth.transform.position.y;
        position[2] = playerhealth.transform.position.z;
    }
}

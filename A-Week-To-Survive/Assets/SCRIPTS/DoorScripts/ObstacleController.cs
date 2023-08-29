using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int health = 100;

    public void DamageObstacle()
    {
        health = health - 10;
        if(health <= 0)

        {
            Destroy(gameObject);
        }

    }

}

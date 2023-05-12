using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructible : MonoBehaviour
{
    public LayerMask destructibleDoor;
    private float range = 5f;
    public bool destructableState = false;

    public GameObject destructableObject;

    public void RayCastBarrier()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, destructibleDoor))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);
            if(hit.collider != null)
            {
                destructableObject = hit.collider.gameObject;
                Debug.Log("Object: " + destructableObject);
                destructableState = true;
            }
            else
            {
                destructableObject = null;
                destructableState = false;   
            }
        }
    }
}

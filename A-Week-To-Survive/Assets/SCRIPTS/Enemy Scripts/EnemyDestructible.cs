using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructible : MonoBehaviour
{
    public LayerMask destructibleDoor;
    private float range = 100f;
    public bool destructableState = false;

    public Transform destructableObject = null;

    public void RayCastBarrier()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, destructibleDoor))
        {
            Debug.Log("Raycasted!");
            
            if(hit.collider != null)
            {
                destructableObject = hit.collider.transform;
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

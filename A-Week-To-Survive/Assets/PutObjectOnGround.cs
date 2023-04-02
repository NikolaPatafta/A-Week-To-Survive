using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PutObjectOnGround : MonoBehaviour
{

    public LayerMask mask;
    float radius;

    private NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();    
        navAgent.enabled = false;

        // set the vertical offset to the object's collider bounds' extends
        if (GetComponent<BoxCollider>() != null)
        {
            
            radius = GetComponent<BoxCollider>().bounds.extents.y;
        }
        else
        {
            radius = 1f;
        }

        // raycast to find the y-position of the masked collider at the transforms x/z
        RaycastHit hit;

        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (hit.collider != null)
            {
                // this is where the gameobject is actually put on the ground
                transform.position = new Vector3(transform.position.x, hit.point.y + radius, transform.position.z);
                navAgent.enabled = true;

            }
        }
    }
}


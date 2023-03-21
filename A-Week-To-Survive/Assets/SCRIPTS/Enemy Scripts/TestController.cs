using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerStats>().transform;

        Debug.Log("Found: " + target);

    }

    private void Update()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        RotateToTarget();
    }

    private void RotateToTarget()
    {
        transform.LookAt(target);
    }

}

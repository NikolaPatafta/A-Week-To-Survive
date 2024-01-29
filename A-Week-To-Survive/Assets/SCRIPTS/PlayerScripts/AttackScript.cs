using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public float damage = 2f;
    public float radius = 1.8f;
    public LayerMask layerMask;
    public LayerMask doorMask;
    public bool attackingPlayer;

    void Update()
    {
        if(attackingPlayer)
        {
            PlayerHit();
        }
        else
        {
            DoorHit();
        }
         
    }

    public void PlayerHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage, hits[0].transform);
            gameObject.SetActive(false);
        }
    }

    public void DoorHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, doorMask);
        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage, hits[0].transform);
            gameObject.SetActive(false);
        }
    }
}

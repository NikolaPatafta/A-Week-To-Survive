using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBlocker : MonoBehaviour
{
    [SerializeField] private GameObject doorTextHolder;
    private DoorRayCast doorRayCast;
    private BoxCollider boxCollider;


    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
        doorRayCast = FindObjectOfType<DoorRayCast>();
        
    }

    private void OnDisable()
    { 
        if(doorTextHolder != null)
        {
            doorTextHolder.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        doorTextHolder.gameObject.SetActive(true);
        if (doorRayCast != null)
        {
            doorRayCast.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        doorTextHolder.gameObject.SetActive(false);
        if (doorRayCast != null)
        {
            doorRayCast.enabled = true;
        }
    }
}

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
        if(doorRayCast != null)
        {
            doorRayCast.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (doorRayCast != null)
        {
            doorRayCast.enabled = true;
        }
        if(doorTextHolder != null)
        {
            doorTextHolder.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        doorTextHolder.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        doorTextHolder.gameObject.SetActive(false);
    }
}

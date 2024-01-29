using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCutsceneHelpScript : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField] private GameObject doorBlocker;
    [SerializeField] private GameObject turnoffloopedTimeline;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            doorBlocker.gameObject.SetActive(false);
            turnoffloopedTimeline.gameObject.SetActive(false);
        }
    }

}

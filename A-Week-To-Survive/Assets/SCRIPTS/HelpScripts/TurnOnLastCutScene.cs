using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLastCutScene : MonoBehaviour
{
    [SerializeField] GameObject lastCutScene;
    private BoxCollider boxCollider;
    [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            lastCutScene.gameObject.SetActive(true);
            boxCollider.enabled = false;  
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
                StartCoroutine(EnableMovement());
            }
        }
    }
    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(15f);
        playerMovement.enabled = true;
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 teleportPosition;

    private void OnEnable()
    {
        teleportPosition = transform.position;
        player = FindObjectOfType<PlayerMovement>().gameObject;

        if(player != null)
        {
            player.transform.position = teleportPosition;   
        }
        else
        {
            Debug.Log("Could not teleport player.");
        }
    }
}

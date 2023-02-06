using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField]
    private float pickupRange;
    [SerializeField]
    private LayerMask pickupLayer;


    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                Debug.Log("Hit: " + hit.transform.name);
            }
        }
    }
}

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
    private Inventory inventory;

    private void Start()
    {

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        inventory = GetComponent<Inventory>();
        //Debug.Log("Found: " + cam.name);

    }


    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                
                Debug.Log("Hit: " + hit.transform.name);
                Weapons newItem = hit.transform.GetComponent<ItemObject>().item as Weapons;
                inventory.AddItem(newItem);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}

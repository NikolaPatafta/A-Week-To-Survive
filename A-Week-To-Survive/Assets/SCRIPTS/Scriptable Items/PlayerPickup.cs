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

    [SerializeField]
    private InventoryManager inventoryManager;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }


    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                PickupWeapons(hit.transform);
            }
        }
    }

    private void PickupWeapons(Transform hit)
    {
        if (hit.transform.GetComponent<ItemObject>().item as Weapons)
        {
            Items newItem = hit.transform.GetComponent<ItemObject>().item as Items;
            inventoryManager.AddItem(newItem);

        }
        else
        {
            Consumable newItem = hit.transform.GetComponent<ItemObject>().item as Consumable;
            if (newItem.types == ConsumableType.Ammo)
            {
                inventoryManager.AddItem(newItem);
            }
            else if (newItem.types == ConsumableType.Medkit)
            {
                inventoryManager.AddItem(newItem);
            }
        }
        Destroy(hit.transform.gameObject);
    }

}

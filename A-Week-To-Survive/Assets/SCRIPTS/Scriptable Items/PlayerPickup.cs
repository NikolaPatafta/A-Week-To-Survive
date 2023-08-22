using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private InventoryManager inventoryManager;


    private float picturealpha;

    private void Start()
    {
        picturealpha = textMeshPro.color.a;  
    }


    private void Update()
    {
        RayCastPickupWeapons();
    }

    private void PickupWeapons(Transform hit)
    {
        if (hit.transform.GetComponent<ItemObject>())
        {
            Debug.Log("adding: " + hit.transform.name);
            Items newItem = hit.transform.GetComponent<ItemObject>().item;
            inventoryManager.AddItem(newItem);

            Destroy(hit.transform.gameObject);
        }
        
    }

    private void RayCastPickupWeapons()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            string currentWeapon = hit.transform.GetComponent<ItemObject>().item.name;
            textMeshPro.text = "Pickup: " + currentWeapon;
            if (picturealpha <= 0)
            {
                picturealpha -= (Time.deltaTime / 10);
                textMeshPro.color -= new Color(0, 0, 0, picturealpha);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupWeapons(hit.transform);
                
            }
        }
        else
        {
            textMeshPro.color = new Color(255, 255, 255, 0);
            picturealpha = 0;
        }
    }

}

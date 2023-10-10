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
    [SerializeField] private LayerMask specialItem;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI pickupLayerText;
    [SerializeField] private TextMeshProUGUI specialLayerText;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private SpecialItemsManager specialItemsManager;


    private float picturealpha;
    private float specialpicturealpha;

    private void Start()
    {
        picturealpha = pickupLayerText.color.a;
        specialpicturealpha = specialLayerText.color.a;
    }


    private void Update()
    {
        RayCastPickupWeapons();
        RayCastSpecialItem();
    }

    private void PickupWeapons(Transform hit)
    {
        if (hit.transform.GetComponent<ItemObject>())
        {
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
            if (picturealpha <= 0)
            {
                picturealpha -= (Time.deltaTime / 10);
                pickupLayerText.color -= new Color(0, 0, 0, picturealpha);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupWeapons(hit.transform);
                
            }
        }
        else
        {
            pickupLayerText.color = new Color(255, 255, 255, 0);
            picturealpha = 0;
        }
    }

    private void RayCastSpecialItem()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, specialItem))
        {
            string currentItem = hit.transform.name;
            specialLayerText.text = "Special item: " + currentItem;
            if (specialpicturealpha <= 0)
            {
                specialpicturealpha -= (Time.deltaTime / 10);
                specialLayerText.color -= new Color(0, 0, 0, specialpicturealpha);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                specialItemsManager.SpecialItemCollected();
                Debug.Log("Picked up: " + currentItem);
                //add special item to inventory!

                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            specialLayerText.color = new Color(255, 255, 255, 0);
            specialpicturealpha = 0;
        }
    }

}

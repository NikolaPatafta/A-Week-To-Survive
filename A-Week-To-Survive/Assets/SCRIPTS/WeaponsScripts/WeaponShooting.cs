using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
    private Items item;
    private Inventory inventory;
    private EquipmentManager equipmentmanager;

    private void Start()
    {
        mainCam = Camera.main;
        inventory = GetComponent<Inventory>();
        equipmentmanager = GetComponent<EquipmentManager>();
    }

    private void Update()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

            }
        }
    }

    private void Shoot()
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height /2));
        RaycastHit hit;

        //if(Physics.Raycast(ray, inventory.GetWeapons(equipmentmanager.))
        {

        }
    }
}

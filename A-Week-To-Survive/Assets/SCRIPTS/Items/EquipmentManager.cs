using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField]
    private Transform WeaponHolder = null;

    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(inventory.GetWeapons(0).prefab, 0);
     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(inventory.GetWeapons(1).prefab, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(inventory.GetWeapons(2).prefab, 2);
        }
    }

    private void EquipWeapon(GameObject weaponObj, int weaponStyle)
    {
        Weapons weapon = inventory.GetWeapons(weaponStyle);
        if (weapon != null)
        {
            Instantiate(weaponObj, WeaponHolder);
        }
    }
}

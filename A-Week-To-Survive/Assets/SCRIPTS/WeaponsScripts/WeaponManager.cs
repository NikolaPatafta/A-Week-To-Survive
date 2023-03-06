using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private Transform weaponHolderPosition;

    private int current_Weapon_Index;

    // Start is called before the first frame update
    void Start()
    {
        //current_Weapon_Index = 0;
        //weapons[current_Weapon_Index].gameObject.SetActive(true);
        //TurnOnSelectedWeapon(6);      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateInventoryItemInHotbar(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateInventoryItemInHotbar(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateInventoryItemInHotbar(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateInventoryItemInHotbar(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivateInventoryItemInHotbar(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ActivateInventoryItemInHotbar(5);
        }
        //ova dva ostala po starom (turnonselectedweapon) metodi
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            TurnOnSelectedWeapon(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            TurnOnSelectedWeapon(7);
        }

    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (current_Weapon_Index == weaponIndex)
        {
            return;
        }
        //unequip current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);

        //equip selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        current_Weapon_Index = weaponIndex; 
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }

    private void ActivateInventoryItemInHotbar(int index)
    {
        Instantiate(inventoryManager.GetCurrentlySelectedItem().prefab, weaponHolderPosition);
    }
}

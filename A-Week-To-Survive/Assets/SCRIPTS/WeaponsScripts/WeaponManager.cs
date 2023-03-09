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

    public Items CurrentlySelctedItem; 

    // Start is called before the first frame update
    void Start()
    {
        CurrentlySelctedItem = inventoryManager.GetCurrentlySelectedItem();
        //current_Weapon_Index = 0;
        //weapons[current_Weapon_Index].gameObject.SetActive(true);
        //TurnOnSelectedWeapon(6);      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }
  

}

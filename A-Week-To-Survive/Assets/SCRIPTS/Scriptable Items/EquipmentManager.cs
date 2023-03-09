using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    
    [SerializeField]
    private Transform WeaponHolder = null;

    [SerializeField]
    private InventoryManager inventoryManager;

    //Equipment manager - Animations for hands https://www.youtube.com/watch?v=HrTrx_98e8s

    private Animator anim = null;

    [SerializeField]
    private GameObject[] Toolbar;

    [SerializeField]
    private GameObject currentlyEquipedWeapon;

    private int currentlySelectedSlot;

    [SerializeField]
    Weapons defaultweapon = null;

    private void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        //Debug.Log("animator: " + anim.name);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());
        }
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());

        }
        if(Input.GetKeyUp(KeyCode.Alpha3))
        {
            EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());

        }
    }

    private void EquipWeapon(Weapons weapon)
    {
        if (WeaponHolder.GetComponentInChildren<Animator>() != null)
        {
            Debug.Log("Weapon Unequiped");
            UnequipWeapon(); 
        }
        else
        {
            currentlyEquipedWeapon = Instantiate(weapon.prefab, WeaponHolder);
            Debug.Log("Instantiated: " + currentlyEquipedWeapon);
        }
          
    }

    private void UnequipWeapon() 
    {
        if (currentlyEquipedWeapon != null )
        {
            Destroy(currentlyEquipedWeapon);
        }
    }
}

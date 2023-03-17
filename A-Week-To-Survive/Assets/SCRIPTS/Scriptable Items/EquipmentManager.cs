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

    [SerializeField]
    private GameObject[] Toolbar;

    [SerializeField]
    private GameObject currentlyEquipedWeapon;

    private PlayerStats playerstats;

    private void Awake()
    {   
        playerstats = GetComponent<PlayerStats>();
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
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());
        }
    }

    private void EquipWeapon(Weapons weapon)
    {
        if (WeaponHolder.GetComponentInChildren<Animator>() != null)
        {
            UnequipWeapon(); 
        }
        else if (inventoryManager.GetCurrentlySelectedWeapon() != null)
        {
            currentlyEquipedWeapon = Instantiate(weapon.prefab, WeaponHolder);
            playerstats.UpdateWeaponUI(weapon);
        }
          
    }

    public GameObject InstantiatedGameObject()
    {
        return currentlyEquipedWeapon;
    }

    private void UnequipWeapon() 
    {
        if (currentlyEquipedWeapon != null )
        {
            Destroy(currentlyEquipedWeapon);
        }
    }
}

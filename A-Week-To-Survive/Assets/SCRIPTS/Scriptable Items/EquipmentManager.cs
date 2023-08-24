using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //Equipment manager - Animations for hands https://www.youtube.com/watch?v=HrTrx_98e8s

    [SerializeField] private Transform WeaponHolder = null;
    [SerializeField] private InventoryManager inventoryManager; 
    [SerializeField] private GameObject[] Toolbar;
    [SerializeField] private GameObject currentlyEquipedWeapon;

    private PlayerStats playerstats;
    public int selectedSlot;
    public bool isWeaponEquiped;

    private void Awake()
    {   
        playerstats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 7)
            {
                EquipWeapon(inventoryManager.GetCurrentlySelectedWeapon());
                selectedSlot = (number - 1);
            }
        }
    }

    public void EquipWeapon(Weapons weapon)
    {
        if (WeaponHolder.GetComponentInChildren<Animator>() != null)
        {
            UnequipWeapon(); 
        }
        else if (inventoryManager.GetCurrentlySelectedWeapon() != null)
        {
            currentlyEquipedWeapon = Instantiate(weapon.prefab, WeaponHolder);
            playerstats.UpdateWeaponUI(weapon);
            isWeaponEquiped = true;
        }     
    }

    public GameObject InstantiatedGameObject()
    {
        return currentlyEquipedWeapon;
    }

    public void UnequipWeapon() 
    {
        if (currentlyEquipedWeapon != null )
        {
            Destroy(currentlyEquipedWeapon);
        }
        isWeaponEquiped = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Weapons[] weapons;

    private PlayerStats playerStats;


    private void Start()
    {
        playerStats= GetComponent<PlayerStats>();
        InitVariables();
    }


    public void AddItem(Weapons newItem)
    {
        int newItemIndex = (int)newItem.weaponType;

        if (weapons[newItemIndex] != null)
        {
            RemoveItem(newItemIndex);
        }
        weapons[newItemIndex] = newItem;

        //Update weaponUI
        playerStats.UpdateWeaponUI(newItem);
  
    }
    public void RemoveItem(int index) 
    {
        weapons[index] = null;

    }
    public Weapons GetWeapons(int index)
    {
        return weapons[index];
    }


    private void InitVariables()
    {
        weapons = new Weapons[3];
    }

}



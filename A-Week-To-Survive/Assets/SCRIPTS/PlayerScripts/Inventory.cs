using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Weapons[] weapons;

    public Weapons testWeapon;
    public Weapons testWeapon2;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            AddItem(testWeapon);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(testWeapon2);
        }
    }

    public void AddItem(Weapons newItem)
    {
        int newWeaponsIndex = (int)newItem.weaponType;

        if (weapons[newWeaponsIndex] != null)
        {
            RemoveItem(newWeaponsIndex);
        }
        weapons[newWeaponsIndex] = newItem;
  
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



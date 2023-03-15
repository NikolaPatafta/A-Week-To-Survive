using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Weapons[] weapons;


    private void Start()
    {
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



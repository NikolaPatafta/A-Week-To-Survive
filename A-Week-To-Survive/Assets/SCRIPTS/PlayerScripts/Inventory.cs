using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Items[] weapons;

    private void Start()
    {
        InitVariables();
    }

    public void AddItem(Items newItem)
    {

    }

    private void InitVariables()
    {
        weapons = new Items[6];
    }
}



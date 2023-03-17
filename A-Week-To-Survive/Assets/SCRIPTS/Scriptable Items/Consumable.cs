using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class Consumable : Items
{
    public ConsumableType types;
}

public enum ConsumableType { Medkit, Ammo}

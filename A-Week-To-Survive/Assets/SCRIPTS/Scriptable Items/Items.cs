using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Items/Item")]
public class Items : ScriptableObject
{
    public string ItemsName;
    public string description;
    public Sprite icon;
    [Header("Gameobject prefab")]
    public GameObject prefab;

    [Header("UI ONLY")]
    public bool stackable = true;

    [Header("Gameplay ONLY")]
    public ItemType type;

}

//WIP
public enum ItemType
{
    Weapon,
    Ammo
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]
public class Weapons : Items
{
    [Header("Weapons only")]
    public int damage;
    public int magazineSize;
    public int storedAmmo;
    public float fireRate;
    public float range;
    public WeaponType weaponType;
    
    [Header("ZoomInOut")]
    public bool zoomInOut = false;
}

public enum WeaponType { Melee, Pistol, AR, Shotgun, Sniper }
public enum WeaponStyle { Primary, Secondary, Third}

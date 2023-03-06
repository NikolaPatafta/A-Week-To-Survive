using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField]
    private Transform WeaponHolder = null;

    private Inventory inventory;

    //Equipment manager - Animations for hands https://www.youtube.com/watch?v=HrTrx_98e8s

    private Animator anim;

    private GameObject currentWeaponObject = null;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha7))
        {
            //if AR is equiped play AR animations
            ChooseWeaponAnimations(0, WeaponType.AR);

            EquipWeapon(inventory.GetWeapons(0).prefab, 0);
        }
        if(Input.GetKeyUp(KeyCode.Alpha8))
        {
            //if Pistol is equiped play pistol animations
            ChooseWeaponAnimations(1, WeaponType.Pistol);

            EquipWeapon(inventory.GetWeapons(1).prefab, 1);

        }
        if(Input.GetKeyUp(KeyCode.Alpha9))
        {
            //if Knife is equiped play knife animations
            ChooseWeaponAnimations(2, WeaponType.Melee);

            EquipWeapon(inventory.GetWeapons(2).prefab, 2);

        }
    }

    private void ChooseWeaponAnimations(int weaponStyle, WeaponType weaponType)
    {
        Weapons weapons = inventory.GetWeapons(weaponStyle);

        if (weapons != null)
        {
            if (weapons.weaponType == weaponType)
            {
                anim.SetInteger("weaponType", (int)weaponType);
            }
        }
    }

    private void InstantiateWeapon(GameObject weaponObj, int weaponStyle)
    {
        Weapons weapon = inventory.GetWeapons(weaponStyle);
        if (weapon != null)
        {
            Instantiate(weaponObj, WeaponHolder);
        }
    }

    private void EquipWeapon(GameObject weaponObject, int weaponStyle)
    {
        Weapons weapons = inventory.GetWeapons(weaponStyle);
        if (weapons != null)
        {
            currentWeaponObject = Instantiate(weaponObject, WeaponHolder);
        }
    }

    private void UnequipWeapon()
    {
        anim.SetTrigger("unequipTrigger");
        Destroy(currentWeaponObject);
    }
}

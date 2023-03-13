using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
    private float lastShootTime = 0f;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private EquipmentManager equipmentManager;
    private WeaponHandler weaponHandler;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
        
    }

    private void RayCastShoot(Weapons currentWeapon)
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height /2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit , currentWeapon.range))
        {
            Debug.Log(hit.transform.name);
        }
    }

    private void Shoot()
    {
        Weapons currentWeapon = inventoryManager.GetCurrentlySelectedWeapon();

        if(Time.time > lastShootTime + currentWeapon.fireRate)
        {
            lastShootTime = Time.time;  

            RayCastShoot(currentWeapon);
            weaponHandler = GetComponentInChildren<WeaponHandler>();
            weaponHandler.ShootAnimation();

        }
    }
}

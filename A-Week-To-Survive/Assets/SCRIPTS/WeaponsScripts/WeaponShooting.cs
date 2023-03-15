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

    //amunition
    //***
    //reload funkcije od pocetka i animacije na 11:15min (https://www.youtube.com/watch?v=ncsOjZHul2U)
    //***

    [SerializeField] private bool canShoot = true;
    public bool canReload = true;   

    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrentAmmoStorage;

    [SerializeField] private bool primaryIsEmpty = false;

    private int secondaryCurrentAmmo;
    private int secondaryCurrentAmmoStorage;

    private Inventory inventory;
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        mainCam = Camera.main;
        inventory = GetComponent<Inventory>();
        canShoot = true;
        canReload = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator = equipmentManager.InstantiatedGameObject().GetComponent<Animator>();
            Reload();
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
        CheckIfCanShoot();

        if (canShoot && canReload)
        {
            Weapons currentWeapon = inventoryManager.GetCurrentlySelectedWeapon();

            if (Time.time > lastShootTime + currentWeapon.fireRate)
            {
                lastShootTime = Time.time;

                RayCastShoot(currentWeapon);
                weaponHandler = GetComponentInChildren<WeaponHandler>();
                weaponHandler.ShootAnimation();
                UseAmmo(1, 0);
            }
        }
        else Debug.Log("Magazine is empty");
    }

    private void UseAmmo(int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        if (primaryCurrentAmmo <= 0)
        {
            primaryIsEmpty = true;
            CheckIfCanShoot();
        }
        else
        {
            primaryCurrentAmmo -= currentAmmoUsed;
            primaryCurrentAmmoStorage -= currentStoredAmmoUsed;
        }
    }

    private void CheckIfCanShoot()
    {
        if(primaryIsEmpty)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }

    public void InitAmmo(InventorySlot slot, Weapons weapon)
    {
        primaryCurrentAmmo = weapon.magazineSize;
        primaryCurrentAmmoStorage = weapon.storedAmmo;

        //secondary
        /*if(slot == 1)
        {
            secondaryCurrentAmmo = weapon.magazineSize;
            secondaryCurrentAmmoStorage = weapon.storedAmmo;
        }*/

    }

    private void Reload()
    {
        if (canReload)
        {
            int ammoToReload = inventoryManager.GetCurrentlySelectedWeapon().magazineSize - primaryCurrentAmmo;
            //ako imamo dovoljno municije za reload
            if (primaryCurrentAmmoStorage >= ammoToReload)
            {
                //ako je magazine full
                if (primaryCurrentAmmo == inventoryManager.GetCurrentlySelectedWeapon().magazineSize)
                {
                    Debug.Log("Magazine is already full!");
                    return;
                }

                primaryCurrentAmmo += ammoToReload;
                primaryCurrentAmmoStorage -= ammoToReload;

                primaryIsEmpty = false;
                CheckIfCanShoot();
                

            }

            animator.SetTrigger("Reload");
        }
        else Debug.Log("Cant reload at the momment");
    }
}

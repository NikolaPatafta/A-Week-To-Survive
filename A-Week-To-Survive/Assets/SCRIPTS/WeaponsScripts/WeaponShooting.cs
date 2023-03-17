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
    [SerializeField]
    private PlayerStats playerStats;

    //amunition
    //***
    //reload funkcije od pocetka i animacije na 11:15min (https://www.youtube.com/watch?v=ncsOjZHul2U)
    //***

    [SerializeField] private bool canShoot = true;
    public bool canReload = true;

    [SerializeField] private int[] CurrentAmmo;
    [SerializeField] private int[] CurrentAmmoStorage;

    [SerializeField] private bool weaponIsEmpty = false;

    private int secondaryCurrentAmmo;
    private int secondaryCurrentAmmoStorage;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        mainCam = Camera.main;
        canShoot = true;
        canReload = true;
        playerStats = GetComponent<PlayerStats>();  
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
            Reload(inventoryManager.selectedInventorySlot);
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
        CheckIfCanShoot(inventoryManager.selectedInventorySlot);

        if (canShoot && canReload)
        {
            Weapons currentWeapon = inventoryManager.GetCurrentlySelectedWeapon();

            if (Time.time > lastShootTime + currentWeapon.fireRate)
            {
                lastShootTime = Time.time;

                RayCastShoot(currentWeapon);
                weaponHandler = GetComponentInChildren<WeaponHandler>();
                weaponHandler.ShootAnimation();
                UseAmmo(inventoryManager.selectedInventorySlot, 1, 0);
            }
        }
        else Debug.Log("Magazine is empty");
    }

    private void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        if (CurrentAmmo[slot] <= 0)
        {
            weaponIsEmpty = true;
            CheckIfCanShoot(inventoryManager.selectedInventorySlot);
        }
        else
        {
            CurrentAmmo[slot] -= currentAmmoUsed;
            CurrentAmmoStorage[slot] -= currentStoredAmmoUsed;
            playerStats.UpdateWeaponAmmoUI(CurrentAmmo[slot], CurrentAmmoStorage[slot]);
        }
    }

    private void CheckIfCanShoot(int slot)
    {
        if(weaponIsEmpty)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }

    //fixati initialize ammo
    public void InitAmmo(int slot, Weapons weapon)
    {
        Debug.Log("Init ammo: " + slot);
        CurrentAmmo[slot] = weapon.magazineSize;
        CurrentAmmoStorage[slot] = weapon.storedAmmo;
    }

    private void AddAmmo(int slot, int currentAmmoAdded, int currentStoredAmmoAdded)
    {
        CurrentAmmo[slot] += currentAmmoAdded;
        CurrentAmmoStorage[slot] += currentStoredAmmoAdded;
    }

    private void Reload(int slot)
    {
        if (canReload)
        {
            int ammoToReload = inventoryManager.GetCurrentlySelectedWeapon().magazineSize - CurrentAmmo[slot];
            //ako imamo dovoljno municije za reload
            if (CurrentAmmoStorage[slot] >= ammoToReload)
            {
                //ako je magazine full
                if (CurrentAmmo[slot] == inventoryManager.GetCurrentlySelectedWeapon().magazineSize)
                {
                    Debug.Log("Magazine is already full!");
                    return;
                }

                AddAmmo(slot, ammoToReload, 0);
                UseAmmo(slot, 0, ammoToReload);

                weaponIsEmpty = false;
                CheckIfCanShoot(slot);
                

            }

            animator.SetTrigger("Reload");
        }
        else Debug.Log("Cant reload at the momment!");
    }
}

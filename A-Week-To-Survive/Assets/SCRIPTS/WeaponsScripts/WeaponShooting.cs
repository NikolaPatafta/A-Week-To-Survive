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
    [SerializeField] private int CurrentAmmoStorage = 0;

    [SerializeField] private bool weaponIsEmpty = false;

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
        if (inventoryManager.GetCurrentlySelectedWeapon() != null)
        {
            if (inventoryManager.GetCurrentlySelectedWeapon().weaponType != WeaponType.Melee)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Shoot(equipmentManager.selectedSlot);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    animator = equipmentManager.InstantiatedGameObject().GetComponent<Animator>();
                    Reload(equipmentManager.selectedSlot);
                }
            }
            else if (inventoryManager.GetCurrentlySelectedWeapon().weaponType == WeaponType.Melee)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    AxeAttack();
                }
            }
            else if (inventoryManager.GetCurrentlySelectedWeapon().weaponType == WeaponType.Bow)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    BowAttack();
                }
            }
        }


        playerStats.UpdateWeaponAmmoUI(CurrentAmmo[equipmentManager.selectedSlot], CurrentAmmoStorage);

        
    }

    private void RayCastShoot(Weapons currentWeapon)
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height /2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit , currentWeapon.range))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Enemy")
            {
                HealthScript healthScript = hit.transform.GetComponent<HealthScript>();
                healthScript.ApplyDamage(currentWeapon.damage);

            }
        }
    }

    private void Shoot(int slot)
    {
        CheckIfCanShoot(equipmentManager.selectedSlot);
        if (CurrentAmmo[slot] <= 0)
        {
            weaponIsEmpty = true;
        }

        if (canShoot && canReload)
        {
            Weapons currentWeapon = inventoryManager.GetCurrentlySelectedWeapon();

            if (Time.time > lastShootTime + currentWeapon.fireRate)
            {
                lastShootTime = Time.time;

                RayCastShoot(currentWeapon);
                weaponHandler = GetComponentInChildren<WeaponHandler>();
                weaponHandler.ShootAnimation();
                UseAmmo(slot, 1, 0);
            }
        }
        else Debug.Log("Magazine is empty");

    }

    private void AxeAttack()
    {
        weaponHandler= GetComponentInChildren<WeaponHandler>();
        weaponHandler.ShootAnimation();
    }

    private void BowAttack()
    {
        weaponHandler = GetComponentInChildren<WeaponHandler>();
        weaponHandler.ShootAnimation();
    }

    private void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        CurrentAmmo[slot] -= currentAmmoUsed;
        CurrentAmmoStorage -= currentStoredAmmoUsed;
    }

    private void AddAmmo(int slot, int currentAmmoAdded, int currentStoredAmmoAdded)
    {
        CurrentAmmo[slot] += currentAmmoAdded;   
    }

    public void InitAmmoSecondaryMagazine(Consumable ammo)
    {
        if(CurrentAmmoStorage == 0) 
        {
            CurrentAmmoStorage = ammo.AmmoCount;
        }
        else
        {
            CurrentAmmoStorage += ammo.AmmoCount;
        }
        
    }

    public void InitAmmoPrimaryMagazine(int slot, Weapons weapon)
    {
        CurrentAmmo[slot] = weapon.magazineSize;   
    }

    private void Reload(int slot)
    {
        if (canReload)
        {
            int ammoToReload = inventoryManager.GetCurrentlySelectedWeapon().magazineSize - CurrentAmmo[slot];
            //ako imamo dovoljno municije za reload
            if (CurrentAmmoStorage >= ammoToReload)
            {
                //ako je magazine full
                if (CurrentAmmo[slot] == inventoryManager.GetCurrentlySelectedWeapon().magazineSize)
                {
                    Debug.Log("Magazine is already full!");
                    return;
                }
                animator.SetTrigger("Reload");
                AddAmmo(slot, ammoToReload, 0);
                UseAmmo(slot, 0, ammoToReload);

                weaponIsEmpty = false;
                CheckIfCanShoot(slot);

            }
            if (CurrentAmmoStorage < ammoToReload && CurrentAmmoStorage != 0)
            {
                animator.SetTrigger("Reload");
                AddAmmo(slot, CurrentAmmoStorage, 0);
                UseAmmo(slot, 0, CurrentAmmoStorage);
                CurrentAmmoStorage = 0;
                
                weaponIsEmpty = false;
                CheckIfCanShoot(slot);
            }
            else if (CurrentAmmoStorage == 0)
            {
                Debug.Log("No ammo to reload!");
            }

            
        }
        else Debug.Log("Cant reload at the momment!");
    }

    private void CheckIfCanShoot(int slot)
    {
        if (CurrentAmmo[slot] <= 0)
        {
            canShoot = false;
            weaponIsEmpty = true;
        }
        else
        {
            canShoot = true;
            weaponIsEmpty = false;
        }
    }
    //old bullet fired method
    /*void BulletFired()
    {

        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                print("We hit " + hit.transform.gameObject.name);

                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }*/
}

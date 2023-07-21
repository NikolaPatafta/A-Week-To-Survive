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
    [SerializeField]
    private GameObject bloodParticles;
    private HealthScript healthScript;

    //amunition
    //***
    //reload funkcije od pocetka i animacije na 11:15min (https://www.youtube.com/watch?v=ncsOjZHul2U)
    //***

    [SerializeField] private bool canShoot = true;
    public bool canReload = true; 
    [SerializeField] public GameObject[] inventorySlots;  
    [SerializeField] private int CurrentAmmoStorage = 0;
    [SerializeField] private bool weaponIsEmpty = false;
    [SerializeField] private Animator animator;

    private void Start()
    {
        mainCam = Camera.main;
        canShoot = true;
        canReload = true;
    }

    private void Update()
    {
        if (inventoryManager.GetCurrentlySelectedWeapon() != null)
        {

            if (inventoryManager.GetCurrentlySelectedWeapon().weaponType == WeaponType.Melee)
            {
                if (inventoryManager.GetCurrentlySelectedWeapon().type == ItemType.Consumable && (Input.GetKey(KeyCode.Mouse0)))
                {
                    ConsumableUse();
                }
                else if (Input.GetKey(KeyCode.Mouse0))
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
            else
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
        }
        if (inventorySlots[equipmentManager.selectedSlot].GetComponentInChildren<InventoryItem>() != null) 
        {
            UpdateWeaponUIInfo(equipmentManager.selectedSlot);
        }
    }

    private void RayCastShoot(Weapons currentWeapon)
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height /2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit , currentWeapon.range))
        {
            //Debug.DrawRay(ray.origin, ray.direction * currentWeapon.range, Color.red, 2f);
            if(hit.transform.tag == "Enemy")
            {
                HealthScript healthScript = hit.transform.GetComponent<HealthScript>();
                healthScript.ApplyDamage(currentWeapon.damage);
                //Spawn blood particles
                SpawnBloodParticles(hit.point, hit.normal);
                EnemyController enemyController = hit.transform.GetComponent<EnemyController>();
                enemyController.CallPlayZombieHurtSound();
            }
        }
    }

    private void Shoot(int slot)
    {
        CheckIfCanShoot(equipmentManager.selectedSlot);
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

    private void ConsumableUse()
    {
        Debug.Log("Added health!");
        healthScript = GetComponentInParent<HealthScript>();    
        healthScript.AddHealth(10);     
        inventoryManager.GetSelectedItem(true);
        equipmentManager.UnequipWeapon();
    }

    private void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        InventoryItem inventoryItem = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        inventoryItem.ammoCount -= currentAmmoUsed;
        CurrentAmmoStorage -= currentStoredAmmoUsed;
    }

    private void AddAmmo(int slot, int currentAmmoAdded, int currentStoredAmmoAdded)
    {
        InventoryItem inventoryItem = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        inventoryItem.ammoCount += currentAmmoAdded; 
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

    private void Reload(int slot)
    {
        if (canReload)
        {
            
            InventoryItem inventoryItem = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
            int ammoToReload = inventoryManager.GetCurrentlySelectedWeapon().magazineSize - inventoryItem.ammoCount;
            
            //ako imamo dovoljno municije za reload
            if (CurrentAmmoStorage >= ammoToReload && CurrentAmmoStorage !>= 0)
            {
                       
                //ako je magazine full
                if (inventoryItem.ammoCount == inventoryManager.GetCurrentlySelectedWeapon().magazineSize)
                {
                    Debug.Log("Magazine is already full!");
                    return;
                }
                animator.SetTrigger("Reload");           
                AddAmmo(slot, ammoToReload, 0);
                UseAmmo(slot, 0, ammoToReload);


                weaponIsEmpty = false;
                CheckIfCanShoot(slot);

            }//reload
            if (CurrentAmmoStorage < ammoToReload && CurrentAmmoStorage != 0)
            {
                int finalReload = CurrentAmmoStorage;
                animator.SetTrigger("Reload");
                AddAmmo(slot, finalReload, 0);
                UseAmmo(slot, 0, finalReload);
                CurrentAmmoStorage = 0;         
                weaponIsEmpty = false;
                CheckIfCanShoot(slot);
            }
            //ako je magazine prazan
            else if (CurrentAmmoStorage == 0)
            {
                Debug.Log("No ammo to reload!");
            }
        }
        else Debug.Log("Cant reload at the momment!");
    }

    private void CheckIfCanShoot(int slot)
    {
        InventoryItem inventoryItem = inventorySlots[slot].GetComponentInChildren<InventoryItem>();

        if (inventoryItem.ammoCount <= 0)
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

    private void UpdateWeaponUIInfo(int slot)
    {
        InventoryItem inventoryItem = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        playerStats.UpdateWeaponAmmoUI(inventoryItem.ammoCount, CurrentAmmoStorage);
    }

    private void SpawnBloodParticles(Vector3 position, Vector3 normal)
    {
        Instantiate(bloodParticles, position, Quaternion.FromToRotation(Vector3.up, normal));
    }

}

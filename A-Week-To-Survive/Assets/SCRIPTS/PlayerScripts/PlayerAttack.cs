using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;
    private GameObject crosshair;

    public bool is_Aiming;
    public bool can_Shoot;
    private bool arrow_flying;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Box_StartPosition;

    [SerializeField]
    private InventoryManager inventoryManager;
    private WeaponHandler weaponHandler;


    void Awake()
    {
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (inventoryManager.GetCurrentlySelectedWeapon() != null)
        {
            if(inventoryManager.GetCurrentlySelectedWeapon().zoomInOut == true && inventoryManager.GetCurrentlySelectedWeapon().weaponType == WeaponType.Bow)
            {    
                WeaponShot();
            }
            ZoomInAndOut();
        }  
    }
    void WeaponShot()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            if (is_Aiming)
            {
                weaponHandler = GetComponentInChildren<WeaponHandler>();
                Weapons weapon = inventoryManager.GetCurrentlySelectedWeapon();
                Debug.Log("can shoot " + can_Shoot);
                if(can_Shoot && Time.time > nextTimeToFire + weapon.fireRate)
                {
                    nextTimeToFire = Time.time;
                    ThrowArrowOrSpear(true);           
                }
                /*else if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                {
                       //spear
                       ThrowArrowOrSpear(false);
                }*/
            }
        }
    }//wep

    //Aim sa kamerom na oruzju
    void ZoomInAndOut()
    {
       //desni click clijamo
        if (Input.GetMouseButtonDown(1))
        {
            zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
            crosshair.SetActive(false);
        }

        //desni click ne ciljamo
        if (Input.GetMouseButtonUp(1))
        {
            zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
            crosshair.SetActive(true);
        }

        
        if(inventoryManager.GetCurrentlySelectedWeapon().weaponType == WeaponType.Bow)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponHandler = GetComponentInChildren<WeaponHandler>();
                weaponHandler.Aim(true);
                is_Aiming= true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponHandler = GetComponentInChildren<WeaponHandler>();
                weaponHandler.Aim(false);
                is_Aiming = false;
            }
        }

    }//ZoomInAndOut

    //ako je throwArrow true onda stvaramo arrow, ako je false onda spear
    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Box_StartPosition.position;

            arrow.GetComponent<ArrowAndBowScript>().Launch(mainCam);    
        }
        else
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Box_StartPosition.position;

            spear.GetComponent<ArrowAndBowScript>().Launch(mainCam);
        }
    }
   
}

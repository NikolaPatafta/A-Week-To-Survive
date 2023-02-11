using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;
    private GameObject crosshair;

    private bool is_Aiming;
    private bool arrow_flying;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Box_StartPosition;

    void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>(); 
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
 

        arrow_flying = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

         WeaponShot();
         ZoomInAndOut();

    }

    void WeaponShot()
    {
        //za assault rifle 
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            //lijevi click (auto-shoot)
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 3.8f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();


                BulletFired();
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {

                //Axe
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG || weapon_Manager.GetCurrentSelectedWeapon().tag ==  Tags.HANDS_TAG)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                //Shoot

                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
                else
                {
                    if (is_Aiming)
                    {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            //arrow

                            ThrowArrowOrSpear(true);
                            

                        }
                        else if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            //spear
                            ThrowArrowOrSpear(false);
                        }
                    }

                }

            }

        }

    }//wep

    void ZoomInAndOut()
    {
        //Aim sa kamerom na oruzju
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_aim == WeaponAim.AIM)
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
        }//aim opcije

        //self aim (bow, spear itd..)
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_aim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);

                is_Aiming= true;

            }

            if (Input.GetMouseButtonUp(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);

                is_Aiming = false;

            }

        }//self aim


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

    /*IEnumerator WaitForNewArrowOrSpear()
    {
        yield return new WaitForSeconds(2.02f);
        arrow_flying= false;
    }*/

    void BulletFired()
    {

        RaycastHit hit;
        
        //Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit);

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            Debug.Log(hit.collider.name);


            Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 10 , Color.red, duration:2f);
            Vector3 dir = hit.transform.position - hit.point;
            Debug.DrawRay(hit.point, dir * 10.0f, Color.green);

            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                
                print("We hit " + hit.transform.gameObject.name);

                
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
        }

        }
    }
    
    private void OnDrawGizmos()
    {
        RaycastHit Hitmark;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out Hitmark))
        {
            Gizmos.DrawSphere(Hitmark.point, 0.5f);
        }
    }

    

  

        
}

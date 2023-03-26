using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public WeaponAim weapon_aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reload_Sound, reload_Sound_2;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_Point;

    private WeaponShooting weaponShooting;

    private PlayerAttack playerAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        weaponShooting = GetComponentInParent<WeaponShooting>();
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    public void ShootAnimation()
    {
        anim.SetTrigger("Shoot");
    }

    public void StopAiming()
    {
        Aim(false);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound()
    {
        shootSound.Play();
    }

    void Stop_ShootSound()
    {
        shootSound.Stop();
    }

    void Play_ReloadSound()
    {
        reload_Sound.Play();
    }

    void Play_ReloadSound2()
    {
        reload_Sound_2.Play();
    }

    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }

    public void StartReload()
    {
        weaponShooting.canReload = false;
    }

    public void EndReload()
    {
        weaponShooting.canReload = true;
    }

    //bow 
    public void CanShoot()
    {
        anim.SetBool("CanShoot", true);
        playerAttack.can_Shoot = true;
    }
    public void CanNotShoot()
    {
        anim.SetBool("CanShoot", false);
        playerAttack.can_Shoot = false;
    }

}

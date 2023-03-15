using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image health_Stats, stamina_Stats;
    [SerializeField]
    private Text Healthtext;

    [SerializeField]
    private WeaponUI weaponUI;


    //Na slici (in-game) imamo fill amount od 0 do 1, zato dijelimo helath sa 100
    public void Display_HealthStats (float healthValue)
    {
        healthValue /= 100f;

        health_Stats.fillAmount= healthValue;

        Healthtext.text = (healthValue*100).ToString();
    }

    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;

        stamina_Stats.fillAmount = staminaValue;
    }

    public void UpdateWeaponUI(Weapons newWeapon)
    {
        weaponUI.UpdateInfo(newWeapon.icon, newWeapon.magazineSize, newWeapon.storedAmmo);
    }

}

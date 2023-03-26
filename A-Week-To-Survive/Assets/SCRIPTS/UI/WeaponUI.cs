using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text magazineSizeText;
    [SerializeField]
    private Text storedAmmoText;
    [SerializeField]
    private Text middle;
    [SerializeField]
    private InventoryManager inventoryManager;

    private void Start()
    {
        icon.gameObject.SetActive(false);
        magazineSizeText.gameObject.SetActive(false);
        storedAmmoText.gameObject.SetActive(false);
        middle.gameObject.SetActive(false);
    }

    public void UpdateInfo(Sprite weaponIcon, int magazineSize, int storedAmmo)
    {
        if(inventoryManager.GetCurrentlySelectedWeapon() == null)
        {
            icon.gameObject.SetActive(false);
            magazineSizeText.gameObject.SetActive(false);
            storedAmmoText.gameObject.SetActive(false);
            middle.gameObject.SetActive(false);
        }
        else if (inventoryManager.GetCurrentlySelectedWeapon() != null && inventoryManager.GetCurrentlySelectedWeapon().weaponType != WeaponType.Melee)
        {
            icon.gameObject.SetActive(true);
            magazineSizeText.gameObject.SetActive(true);
            storedAmmoText.gameObject.SetActive(true);
            middle.gameObject.SetActive(true);
            icon.sprite = weaponIcon;
            magazineSizeText.text = magazineSize.ToString();
            storedAmmoText.text = storedAmmo.ToString();
        }
        else if (inventoryManager.GetCurrentlySelectedWeapon() != null && inventoryManager.GetCurrentlySelectedWeapon().weaponType == WeaponType.Melee)
        {
            icon.gameObject.SetActive(true);
            magazineSizeText.gameObject.SetActive(false);
            storedAmmoText.gameObject.SetActive(false);
            middle.gameObject.SetActive(false);
            icon.sprite = weaponIcon;
        }

    }

    public void UpdateAmmoUI(int magazineSize, int storedAmmo)
    {
        if(magazineSizeText.isActiveAndEnabled && storedAmmoText.isActiveAndEnabled)
        {
            magazineSizeText.text = magazineSize.ToString();
            storedAmmoText.text = storedAmmo.ToString();
        }
    }


  
}

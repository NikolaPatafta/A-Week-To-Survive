using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Weapons weapon;

    [Header("InventoryItems")]
    public Image image;
    public Text countText;
    public int ammoCount = 0;

    [HideInInspector]
    public Items item;

    [HideInInspector]
    public int count = 1; 

    [HideInInspector]
    public Transform parentAfterDrag;


    public void CheckIfWeapon()
    {
        if (item.type == ItemType.Weapon && item != null)
        {
            weapon = item as Weapons;
            ammoCount = weapon.magazineSize;
        }
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }


    public void InitialiseItem(Items newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
        RefreshCount();
        CheckIfWeapon();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }


}

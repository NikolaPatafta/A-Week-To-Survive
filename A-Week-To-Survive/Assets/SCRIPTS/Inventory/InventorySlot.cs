using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IDataPersistence
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }  

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    public void LoadData(GameData data)
    {
        InventoryItem invItem = GetComponentInChildren<InventoryItem>();
        if(invItem != null )
        {
            invItem = data.inventoryItem;
        }
    }

    public void SaveData(ref GameData data)
    {
        InventoryItem invItem = GetComponentInChildren<InventoryItem>();
        if(invItem != null)
        {
            data.inventoryItem = invItem;
        }
    }

    public void TestLoadData()
    {
        InventoryItem invItem = GetComponentInChildren<InventoryItem>();
        if (invItem != null)
        {
            Debug.Log("Item: " + invItem);  
        }
    }

    public void TestSaveData()
    {
        InventoryItem invItem = GetComponentInChildren<InventoryItem>();
        if (invItem != null)
        {
            Debug.Log("Item: " + invItem);
        }
    }

}

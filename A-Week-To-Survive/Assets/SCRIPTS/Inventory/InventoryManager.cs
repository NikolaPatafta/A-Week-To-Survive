using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    public GameObject Hotbar;
    public GameObject Inventory;
    public GameObject Crosshair;
    
    public Items currentlySelectedItem;
    public InventorySlot inventorySlot;
    public UIManager uiManager;

    public GameObject player;

    public GameObject inventoryItemPrefab;

    public int maxStackOnItems = 5;
    public bool isInventoryOn;

    [SerializeField] private WeaponShooting weaponShooting;

    //boje 
    int selectedSlot = 0;
    public int selectedInventorySlot = 0;

    private void Awake()
    {
        Hotbar.gameObject.SetActive(true);
        Inventory.gameObject.SetActive(false);
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 7) 
            {
                ChangeSelectedSlot(number - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Inventory.gameObject.activeInHierarchy)
            {
                TurnInventoryOfforOn(false);
            }
            else
            {
                TurnInventoryOfforOn(true);
            }
                
        }
    }

    public void TurnInventoryOfforOn(bool state)
    {
        Inventory.SetActive(state);
        Crosshair.SetActive(!state);
        Cursor.visible = state;
        isInventoryOn = state;

        if (!isInventoryOn && !uiManager.isPaused)
        {
            uiManager.LockCursor();
        }
        else if (isInventoryOn && !uiManager.isPaused)
        {
            uiManager.UnlockCursor();
        }
        
        player.GetComponent<WeaponShooting>().enabled = !state;   
    }

    //Mjenanje boja tijekom selected itema
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Items item)
    {
        //Provjeri ako bilo koji inventory slot ima isti item 
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            //ako nije null i ako je isti item koji trazimo i manji od max numbera
            //onda dodaj item na item (itemstack)
            if (itemInSlot != null && itemInSlot.item == item && 
                itemInSlot.count < maxStackOnItems && itemInSlot.item.stackable == true)
            {
                if (item.type == ItemType.Ammo)
                {
                    weaponShooting.InitAmmoSecondaryMagazine(item as Consumable);
                }
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }

        }
        //Pronaði prazan utor
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            selectedInventorySlot = i;
            InventorySlot slot = inventorySlots[i]; 
            // Debug.Log("inv slot: " + i);
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }

        }
        return false;

    }
    void SpawnNewItem(Items item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        currentlySelectedItem = item;
        inventorySlot = slot;
        if(item.type == ItemType.Ammo)
        {
            weaponShooting.InitAmmoSecondaryMagazine(item as Consumable);
        }


    }

    public Items GetCurrentlySelectedItem()
    {
        return GetSelectedItem(false);
    }

    public Weapons GetCurrentlySelectedWeapon()
    {
        return GetSelectedItem(false) as Weapons;
    }

    public Items GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Items item = itemInSlot.item;
            if(use == true)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject); 
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return itemInSlot.item;
        }
        return null;
    }

    //testtttttttttttttinggggggggggggggggg

    public void CheckForChildren()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot inventorySlot = inventorySlots[i];

            if (inventorySlot != null)
            {
                // Iterate through the child objects of the InventorySlot
                foreach (Transform child in inventorySlot.transform)
                {
                    // Check if the child object has a component of type InventoryItem
                    InventoryItem inventoryItem = child.GetComponent<InventoryItem>();

                    if (inventoryItem != null)
                    {
                        // The InventorySlot has a child of type InventoryItem
                        Debug.Log($"InventoryItem found in InventorySlot {i}, Child: {child.name}");
                        // Optionally, you can perform additional actions here
                    }
                }
            }
        }
    }
}

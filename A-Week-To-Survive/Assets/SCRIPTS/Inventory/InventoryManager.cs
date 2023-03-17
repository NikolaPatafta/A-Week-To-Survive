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

    public GameObject player;

    private Camera cam;

    public GameObject inventoryItemPrefab;

    public int maxStackOnItems = 5;

    [SerializeField]
    private WeaponShooting weaponShooting;

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
        //provjeravamo input od 1-7 za inventory slotove
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
            TurnInventoryOfforOn();
        }
    }

    void TurnInventoryOfforOn()
    {
        if(Inventory.gameObject.activeInHierarchy)
        {
            Inventory.SetActive(false);
            Crosshair.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerAttack>().enabled = true;   
        }
        else
        {
            Inventory.SetActive(true);
            Crosshair.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<PlayerAttack>().enabled = false;

        }
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
            //ako nije null i ako je isti item koji trazimo i manji od max numbera onda dodaj item na item (itemstack)
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackOnItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }

        }
        //Pronaði prazan slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            selectedInventorySlot = i;
            InventorySlot slot = inventorySlots[i]; 
            Debug.Log("inv slot: " + i);
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
        weaponShooting.InitAmmo(selectedInventorySlot, item as Weapons);
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
}

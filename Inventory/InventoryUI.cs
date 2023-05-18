using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    public GameObject controllerUI;

    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //if inventory is open, close it

        inventoryUI.SetActive(false);
    }

    void Update()
    {
        ToggleInventory();
        if (inventoryUI.activeSelf)
        {
            controllerUI.SetActive(false);
        }
        else
        {
            controllerUI.SetActive(true);
        }
    }

    public void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

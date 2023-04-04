using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject leftMobileStickUI;

    public GameObject rightMobileStickUI;

    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //if inventory is open, close it
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }

        //if not on android, disable the right mobile stick
        if (Application.platform != RuntimePlatform.Android)
        {
            rightMobileStickUI.SetActive(false);
        }
    }

    void Update()
    {
        ToggleInventory();
        if (inventoryUI.activeSelf)
        {
            leftMobileStickUI.SetActive(false);
            rightMobileStickUI.SetActive(false);
        }
        else
        {
            leftMobileStickUI.SetActive(true);
            // rightMobileStickUI.SetActive(true);
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (inventoryUI.activeSelf)
            {
                leftMobileStickUI.SetActive(false);
                rightMobileStickUI.SetActive(false);
            }
            else
            {
                leftMobileStickUI.SetActive(true);
                rightMobileStickUI.SetActive(true);
            }
        }
    }

    public void ToggleInventory()
    {
        if (Input.GetButtonDown("Inventory"))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion



    Equipment[] currentEquipment;

    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    //Get a reference to the player's hand
    public GameObject LeftHand;
    public GameObject RightHand;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        print("slotIndex: " + slotIndex);
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            print("one");
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            print("two");

            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        print("three");

        //get gameObject from the new item
        currentEquipment[slotIndex] = newItem;

        //if item is a weapon, equip it in the player's lefthand
        if (newItem.equipSlot == EquipmentSlot.Shield)
        {
            //get gameObject from the new item
            GameObject newGameObject = Instantiate<GameObject>(
                newItem.gameObject,
                LeftHand.transform.position,
                LeftHand.transform.rotation
            );
            newGameObject.SetActive(true);
            //place the new item in the player's lefthand
            newGameObject.transform.parent = LeftHand.transform;
            //set the new item's position to the player's lefthand
            newGameObject.transform.localPosition = Vector3.zero;

            //adjust angle of shield
            newGameObject.transform.localRotation = Quaternion.Euler(0, 270, 90);
            //set scale to 1
            newGameObject.transform.localScale = Vector3.one;
        }
        //if item is a shield, equip it in the player's righthand
        if (newItem.equipSlot == EquipmentSlot.Weapon)
        {
            //get gameObject from the new item
            GameObject newGameObject = Instantiate<GameObject>(
                newItem.gameObject,
                RightHand.transform.position,
                RightHand.transform.rotation
            );
            newGameObject.SetActive(true);
            //place the new item in the player's righthand
            newGameObject.transform.parent = RightHand.transform;
            //set the new item's position to the player's righthand
            newGameObject.transform.localPosition = Vector3.zero;
            //set scale to 1
            newGameObject.transform.localScale = Vector3.one;
        }
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}

using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        // bool wasPickedUp = Inventory.instance.Add(item);
        bool wasPickedUp = FindObjectOfType<Inventory>().Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}

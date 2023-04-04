using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryUI inventoryUI;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OpenInventory);
    }

    void OpenInventory()
    {
        Debug.Log("Open Inventory");
    }
}

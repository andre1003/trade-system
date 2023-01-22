using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    // Slot UI prefab
    public GameObject slotUI;

    // Player inventory UI
    public GameObject playerInventoryUI;


    /// <summary>
    /// Setup player inventory.
    /// </summary>
    /// <param name="playerInventory">Player inventory reference.</param>
    public void SetupInventoryUI(Inventory playerInventory)
    {
        // Player inventory
        foreach(var slot in playerInventory.slots)
        {
            GameObject newSlot = Instantiate(slotUI, playerInventoryUI.transform);
            SlotUI slotUIComponent = newSlot.GetComponent<SlotUI>();
            slotUIComponent.SetupSlot(slot.item, slot.amount.ToString());
            slotUIComponent.button.onClick.AddListener(slotUIComponent.Use);
        }
    }

    /// <summary>
    /// Close the inventory UI.
    /// </summary>
    public void Close()
    {
        // Clear inventories
        ClearInventory();

        // Free player movement
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().SetIsBusy(false);

        // Disable canvas component
        gameObject.GetComponent<Canvas>().enabled = false;
    }


    /// <summary>
    /// Clear inventory slots.
    /// </summary>
    private void ClearInventory()
    {
        // Clear player inventory
        for(int i = playerInventoryUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(playerInventoryUI.transform.GetChild(i).gameObject);
        }
    }
}

using UnityEngine;


public class TradeUI : MonoBehaviour
{
    // Slot UI prefab
    public GameObject slotUI;

    // Player and NPC inventories UIs
    public GameObject playerInventoryUI;
    public GameObject npcInventoryUI;


    // Trade system
    private TradeSystem tradeSystem;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // If trade system is null, find it
        if(tradeSystem == null)
        {
            tradeSystem = GameObject.Find("TradeSystem").GetComponent<TradeSystem>();
        }
    }


    /// <summary>
    /// Setup inventory UI, based on both player and NPC inventories.
    /// </summary>
    /// <param name="playerInventory">Player inventory.</param>
    /// <param name="npcInventory">NPC inventory.</param>
    public void SetupInventoryUI(Inventory playerInventory, Inventory npcInventory)
    {
        // Player inventory
        foreach(var slot in playerInventory.slots)
        {
            GameObject newSlot = Instantiate(slotUI, playerInventoryUI.transform);
            SlotUI slotUIComponent = newSlot.GetComponent<SlotUI>();
            slotUIComponent.SetupSlot(slot.item, slot.amount.ToString());
            slotUIComponent.SetButtonBind(false);

        }

        // NPC inventory
        foreach(var slot in npcInventory.slots)
        {
            GameObject newSlot = Instantiate(slotUI, npcInventoryUI.transform);
            SlotUI slotUIComponent = newSlot.GetComponent<SlotUI>();
            slotUIComponent.SetupSlot(slot.item, slot.amount.ToString());
            slotUIComponent.SetButtonBind(true);
        }
    }

    /// <summary>
    /// Close the trade system UI.
    /// </summary>
    public void Close()
    {
        // Clear inventories
        ClearInventories();

        // Free player movement
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().SetIsBusy(false);

        // Disable canvas component
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Clear both player and NPC inventory.
    /// </summary>
    public void ClearInventories()
    {
        // Player inventory
        for(int i = playerInventoryUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(playerInventoryUI.transform.GetChild(i).gameObject);
        }

        // NPC inventory
        for(int i = npcInventoryUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(npcInventoryUI.transform.GetChild(i).gameObject);
        }
    }
}

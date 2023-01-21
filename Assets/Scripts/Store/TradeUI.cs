using UnityEngine;


public class TradeUI : MonoBehaviour
{
    // Item
    public Item item;

    // Slot UI
    public GameObject slotUI;

    // Player and NPC inventories UIs
    public Transform playerInventoryUI;
    public Transform npcInventoryUI;


    // Trade system
    private TradeSystem tradeSystem;


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
            GameObject newSlot = Instantiate(slotUI, playerInventoryUI);
            SlotUI slotUIComponent = newSlot.GetComponent<SlotUI>();
            slotUIComponent.SetupSlot(slot.item.icon, slot.amount.ToString());
            item = slot.item;
            slotUIComponent.button.onClick.AddListener(Sell);

        }

        // NPC inventory
        foreach(var slot in npcInventory.slots)
        {
            GameObject newSlot = Instantiate(slotUI, npcInventoryUI);
            SlotUI slotUIComponent = newSlot.GetComponent<SlotUI>();
            slotUIComponent.SetupSlot(slot.item.icon, slot.amount.ToString());
            item = slot.item;
            slotUIComponent.button.onClick.AddListener(Buy);
        }
    }

    /// <summary>
    /// Clear both player and NPC inventory.
    /// </summary>
    public void ClearInventories()
    {
        // Player inventory
        while(playerInventoryUI.childCount > 0)
        {
            Transform slot = playerInventoryUI.GetChild(0).parent = null;
            Destroy(slot);
        }

        // NPC inventory
        while(npcInventoryUI.childCount > 0)
        {
            Transform slot = npcInventoryUI.GetChild(0).parent = null;
            Destroy(slot);
        }
    }

    /// <summary>
    /// Buy method.
    /// </summary>
    public void Buy()
    {
        tradeSystem.BuyItem(item);
    }

    /// <summary>
    /// Sell method.
    /// </summary>
    public void Sell()
    {
        tradeSystem.SellItem(item);
    }
}

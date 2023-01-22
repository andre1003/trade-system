using UnityEngine;


public class TradeSystem : MonoBehaviour
{
    // Trade canvas
    public Canvas tradeCanvas;

    // Trade UI
    public TradeUI tradeUI;

    // Audio source and clips
    public AudioSource audioSource;
    public AudioClip buyClip;
    public AudioClip sellClip;


    // Player controller
    private PlayerController playerController;

    // Player and NPC inventory
    private Inventory playerInventory;
    private Inventory npcInventory;

    
    /// <summary>
    /// Buy an item from shopkeeper.
    /// </summary>
    /// <param name="item">Item to be purchased.</param>
    public void BuyItem(Item item)
    {
        // If player can buy the item
        if(playerController.GetCoins() >= item.price)
        {
            // Remove coins from player, add the item to player's inventory and remove it from NPC inventory
            playerController.RemoveCoins(item.price);
            playerInventory.AddItem(item);
            npcInventory.RemoveItem(item);
            
            // Update trade system UI
            UpdateUI();

            // Play buy audio
            audioSource.clip = buyClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Sell an item to shopkeeper.
    /// </summary>
    /// <param name="item">Item to be sold.</param>
    public void SellItem(Item item)
    {
        // Add coins to player, add the item to NPC inventory and remove it from player's inventory
        playerController.AddCoins(item.price);
        npcInventory.AddItem(item);
        playerInventory.RemoveItem(item);

        // Update trade system UI
        UpdateUI();

        // Play buy audio
        audioSource.clip = sellClip;
        audioSource.Play();
    }

    /// <summary>
    /// Set both player and NPC inventories.
    /// </summary>
    /// <param name="playerInventory">Player inventory.</param>
    /// <param name="npcInventory">NPC inventory.</param>
    public void SetInventories(Inventory playerInventory, Inventory npcInventory)
    {
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;
    }

    /// <summary>
    /// Set local PlayerController reference.
    /// </summary>
    /// <param name="playerController">Player controller.</param>
    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    /// <summary>
    /// Setup trade UI and display it to player.
    /// </summary>
    public void DisplayUI()
    {
        tradeUI.SetupInventoryUI(playerInventory, npcInventory);
        tradeCanvas.enabled = true;
    }

    /// <summary>
    /// Update both player and NPC inventories when player buy or sell an item.
    /// </summary>
    private void UpdateUI()
    {
        tradeUI.ClearInventories();
        tradeUI.SetupInventoryUI(playerInventory, npcInventory);
    }
}

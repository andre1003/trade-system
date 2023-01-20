using UnityEngine;


public class TradeSystem : MonoBehaviour
{
    // Player and NPC game objects
    public GameObject player;
    public GameObject shopkeeper;


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
    }
}

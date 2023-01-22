using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    // Item list
    public List<InventorySlot> slots = new List<InventorySlot>();

    // Inventory canvas
    public Canvas inventoryCanvas;


    // Equipped items
    private Item equippedArmor;
    private Item equippedHelmet;
    private Item equippedWeapon;

    // Player controller reference
    private PlayerController playerController;


    void Start()
    {
        // Get player controller reference
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Add an item to inventory.
    /// </summary>
    /// <param name="item">Item to add.</param>
    public void AddItem(Item item)
    {
        // Loop all inventory slots
        foreach(InventorySlot slot in slots)
        {
            // If the item already exists on inventory, increase the amount an exit
            if(slot.item == item)
            {
                slot.amount++;
                return;
            }
        }

        // If the item does not exist on inventory, create a new slot and add to slots list
        InventorySlot newSlot = new InventorySlot(item, 1);
        slots.Add(newSlot);
    }

    /// <summary>
    /// Remove an item from inventory.
    /// </summary>
    /// <param name="item">Item to be removed.</param>
    public void RemoveItem(Item item)
    {
        int index = -1;

        // Loop all inventory slots
        foreach(InventorySlot slot in slots)
        {
            // If the item exists on inventory, get it's index and break loop
            if(slot.item == item)
            {
                index = slots.IndexOf(slot);
                break;
            }
        }

        // If the item does not exist on inventory, exit
        if(index == -1)
        {
            return;
        }

        // If the item amount is bigger than one, just decrease it
        if(slots[index].amount > 1)
        {
            slots[index].amount--;
        }

        // If there are just one item, remove this slot from list
        else
        {
            slots.RemoveAt(index);
        }
    }

    /// <summary>
    /// Add an item with a certain amount.
    /// </summary>
    /// <param name="item">Item to add.</param>
    /// <param name="amount">Amount to add.</param>
    public void AddItemWithAmount(Item item, int amount)
    {
        // Loop all inventory slots
        foreach(InventorySlot slot in slots)
        {
            // If the item already exists on inventory, increase the amount an exit
            if(slot.item == item)
            {
                slot.amount += amount;
                return;
            }
        }

        // If the item does not exist on inventory, create a new slot and add to slots list
        InventorySlot newSlot = new InventorySlot(item, amount);
        slots.Add(newSlot);
    }

    /// <summary>
    /// Display or hide inventory UI.
    /// </summary>
    /// <returns>TRUE if success. FALSE if failed.</returns>
    public bool DisplayOrHide()
    {
        // If there is no inventory canvas, return false
        if(inventoryCanvas == null)
        {
            return false;
        }

        // Get InventoryUI component
        InventoryUI inventoryUI = inventoryCanvas.GetComponent<InventoryUI>();

        // If the inventory UI is NOT being displayed
        if(!inventoryCanvas.enabled)
        {
            // Setup UI and display the UI
            inventoryUI.SetupInventoryUI(this);
            inventoryCanvas.enabled = true;
        }

        // If the inventory UI is being displayed
        else
        {
            // Clear and hide inventory UI
            inventoryUI.Close();
        }
        
        // Return success
        return true;
    }

    /// <summary>
    /// Get additional damage of an equipped weapon.
    /// </summary>
    /// <returns>Additional damage from item.</returns>
    public int GetAdditionalDamageOfItem()
    {
        int additionalDamage = 0;

        if(equippedWeapon != null)
        {
            additionalDamage += equippedWeapon.additionalDamage;
        }

        return additionalDamage;
    }

    /// <summary>
    /// Equip an item.
    /// </summary>
    /// <param name="item">Item to equip.</param>
    public void EquipItem(Item item)
    {
        // Switch item type
        switch(item.type)
        {
            // Armor
            case Item.Type.Armor:
                playerController.BuffHealth(item.additionalHealth);
                equippedArmor = item;
                break;

            // Helmet
            case Item.Type.Helmet:
                playerController.BuffHealth(item.additionalHealth);
                equippedHelmet = item;
                break;

            // Weapon
            case Item.Type.Weapon:
                equippedWeapon = item;
                break;
        }
    }

    /// <summary>
    /// Unequip an item, based on index variable.
    /// </summary>
    /// <param name="index">0 - Armor. 1 - Helmet. 2 - Weapon.</param>
    public void UnequipItem(int index)
    {
        switch(index)
        {
            case 0:
                playerController.DebuffHealth(equippedArmor.additionalHealth);
                equippedArmor = null;
                break;

            case 1:
                playerController.DebuffHealth(equippedHelmet.additionalHealth);
                equippedHelmet = null;
                break;

            case 2:
                equippedWeapon = null;
                break;
        }
    }
}

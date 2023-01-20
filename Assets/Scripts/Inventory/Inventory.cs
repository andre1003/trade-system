using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    // Item list
    private List<InventorySlot> slots = new List<InventorySlot>();


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
    /// Get all items on inventory.
    /// </summary>
    /// <returns>Saved items.</returns>
    public List<InventorySlot> GetSlots()
    { 
        return slots;
    }
}

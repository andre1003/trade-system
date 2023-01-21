using System.Collections.Generic;
using UnityEngine;


public class ItemRandomizer : MonoBehaviour
{
    // Items to randomize
    public List<Item> items;

    // Amounts (items[index] has amounts[index] amount)
    public List<int> amounts;

    // Number of items to add on inventory
    public int numberOfItems = 4;

    // Inventory reference
    public Inventory inventory;


    void Start()
    {
        // Loop number of items
        for(int i = 0 ; i < numberOfItems; i++)
        {
            // Declare number variable
            int index;
            
            // Get random number that is not in the list
            index = Random.Range(0, items.Count);

            // Add the item with a certain amount
            inventory.AddItemWithAmount(items[index], amounts[index]);
        }
    }
}

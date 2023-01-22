public class InventorySlot
{
    // Item
    public Item item;

    // Amount
    public int amount;


    /// <summary>
    /// InventorySlot constructor.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <param name="amount">Amount.</param>
    public InventorySlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SlotUI : MonoBehaviour
{
    // Item icon and amount
    public Image icon;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI priceText;

    // Slot button
    public Button button;

    // Information prefab
    public GameObject informationPrefab;


    // Trade system
    private TradeSystem tradeSystem;

    // Item reference
    private Item item;

    // Item information reference
    private GameObject info;


    /// <summary>
    /// Setup the slot information.
    /// </summary>
    /// <param name="item">Item reference.</param>
    /// <param name="amountText">Amount text.</param>
    public void SetupSlot(Item item, string amountText)
    {
        this.item = item;
        this.icon.sprite = item.icon;
        this.amountText.text = amountText;
        this.priceText.text = item.price.ToString().PadLeft(3, '0');
    }

    /// <summary>
    /// Set button bind, based on isBuy parameter.
    /// </summary>
    /// <param name="isBuy">The bind is for Buy method?</param>
    public void SetButtonBind(bool isBuy)
    {
        // Bind to Buy
        if(isBuy)
        {
            button.onClick.AddListener(Buy);
        }

        // Bind to Sell
        else
        {
            button.onClick.AddListener(Sell);
        }

        // If there is a bind to slot button, the trade system is needed, so find it
        tradeSystem = GameObject.Find("TradeSystem").GetComponent<TradeSystem>();
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

    /// <summary>
    /// Use item method.
    /// </summary>
    public void Use()
    {
        // Get sockets and declare item object
        GameObject sockets = GameObject.Find("Sockets");
        int index = -1;

        // Switch item type
        switch(item.type)
        {
            // Armor
            case Item.Type.Armor:
                index = 0;
                break;

            // Helmet
            case Item.Type.Helmet:
                index = 1;
                break;

            // Weapon
            case Item.Type.Weapon:
                index = 2;
                break;
        }

        // Declar is removing item as false
        bool isRemovingItem = false;

        // If there are any items in socket child
        if(sockets.transform.GetChild(index).childCount > 0)
        {
            // If the item is the same that this.item, set isRemoving item to true
            if(sockets.transform.GetChild(index).GetChild(0).name.Contains(item.prefab.name))
            {
                isRemovingItem = true;
            }

            // Clear this socket children
            ClearSocket(sockets.transform.GetChild(index));
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().UnequipItem(index);
        }

        // If is not removing the item, instantiate the item prefab on the correct socket
        if(!isRemovingItem)
        {
            Instantiate(item.prefab, sockets.transform.GetChild(index));
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().EquipItem(item);
        }
    }

    /// <summary>
    /// Display item information to player.
    /// </summary>
    public void DisplayItemInformation()
    {
        Vector3 position = transform.position;
        position.y -= 300f;

        // Create information
        info = Instantiate(informationPrefab);
        info.transform.parent = transform.parent.parent.parent.parent;
        info.GetComponent<RectTransform>().position = position;
        info.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
        
        info.GetComponent<ItemInformation>().SetInformation(item);
    }

    /// <summary>
    /// Hide item information.
    /// </summary>
    public void HideItemInformation()
    {
        Destroy(info);
    }

    // Called when begin destroy
    void OnDestroy()
    {
        Destroy(info);
    }


    /// <summary>
    /// Clear all children of a given socket.
    /// </summary>
    /// <param name="socket">Socket to be cleared.</param>
    private void ClearSocket(Transform socket)
    {
        // Loop socket child and destroy everything
        for(int i = socket.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(socket.transform.GetChild(i).gameObject);
        }
    }
}

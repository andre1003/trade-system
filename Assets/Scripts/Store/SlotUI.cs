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


    // Trade system
    private TradeSystem tradeSystem;

    // Item reference
    private Item item;


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
        this.tradeSystem = GameObject.Find("TradeSystem").GetComponent<TradeSystem>();
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

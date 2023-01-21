using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SlotUI : MonoBehaviour
{
    // Item icon and amount
    public Image icon;
    public TextMeshProUGUI amountText;

    // Slot button
    public Button button;


    /// <summary>
    /// Setup the slot information.
    /// </summary>
    /// <param name="icon">Item icon.</param>
    /// <param name="amountText">Item amount.</param>
    public void SetupSlot(Sprite icon, string amountText)
    {
        this.icon.sprite = icon;
        this.amountText.text = amountText;
    }
}

using TMPro;
using UnityEngine;


public class ItemInformation : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI additionalHealthText;
    public TextMeshProUGUI additionalDamageText;


    /// <summary>
    /// Set information based on an item.
    /// </summary>
    /// <param name="item">Item to display information.</param>
    public void SetInformation(Item item)
    {
        nameText.text = item.name;
        descriptionText.text = item.description;
        additionalHealthText.text = "Additional Health: " + item.additionalHealth.ToString();
        additionalDamageText.text = "Additional Damage: " + item.additionalDamage.ToString();
    }
}

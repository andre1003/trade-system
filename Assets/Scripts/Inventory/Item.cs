using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    // Item type enumeration
    public enum Type { Armor, Helmet, Weapon }


    // Name and description
    public new string name;
    public string description;

    // Icon
    public Sprite icon;

    // Price
    public int price;

    // Type
    public Type type;

    // Prefab
    public GameObject prefab;

    // Additional stats
    public float additionalHealth;
    public int additionalDamage;
}

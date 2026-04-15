using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Misc
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public GameObject equippedPrefab;
    public string itemName;
    public Sprite icon;

    public bool isStackable;
    public int maxStack = 10;

    public ItemType itemType;
    public int value; // heal amount etc.
}
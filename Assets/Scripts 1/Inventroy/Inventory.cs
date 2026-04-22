using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapon Offset")]
    [SerializeField] private Vector3 weaponPositionOffset;
    [SerializeField] private Vector3 weaponRotationOffset;

    public List<InventorySlot> slots = new List<InventorySlot>();

    [SerializeField] private int maxSlots = 10;

    public Transform handTransform;
    public Health health;
    public InventoryUI inventoryUI;

    private GameObject currentWeapon;

  
    public bool AddItem(ItemData item, int amount = 1)
    {
       
        if (item == null)
        {
            Debug.LogError("AddItem called with NULL item!");
            return false;
        }

        
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot != null && slot.item == item)
                {
                    if (slot.quantity < item.maxStack)
                    {
                        slot.quantity += amount;
                        inventoryUI.RefreshUI();
                        return true;
                    }
                }
            }
        }

      
        if (slots.Count >= maxSlots)
        {
            Debug.Log("Inventory Full");
            return false;
        }

        InventorySlot newSlot = new InventorySlot
        {
            item = item,
            quantity = amount
        };

        slots.Add(newSlot);

        inventoryUI.RefreshUI();

        return true;
    }

    public void EquipItem(ItemData item)
    {
      
        if (item == null || item.equippedPrefab == null)
            return;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(item.equippedPrefab, handTransform);

        currentWeapon.transform.localPosition = weaponPositionOffset;
        currentWeapon.transform.localRotation = Quaternion.Euler(weaponRotationOffset);
    }

  
    public void UseItem(InventorySlot slot)
    {
        if (slot == null || slot.item == null)
            return;

        switch (slot.item.itemType)
        {
            case ItemType.Consumable:
                UseConsumable(slot.item);
                break;
        }

        slot.quantity--;

        if (slot.quantity <= 0)
        {
            slots.Remove(slot);
        }

        inventoryUI.RefreshUI();
    }
    void UseConsumable(ItemData item)
    {
        Debug.Log($"UseConsumable called - Item: {item.itemName}, Value: {item.value}, Health: {(health != null ? health.currentHealth.ToString() : "NULL")}");

        if (health == null)
        {
            Debug.LogError("Health reference is NULL! Assign it in the Inspector on the Inventory script.");
            return;
        }

        if (item == null)
        {
            Debug.LogError("Item is NULL!");
            return;
        }

        health.Heal(item.value);
        Debug.Log($"Healed for {item.value}. New health: {health.currentHealth}");
    }
}
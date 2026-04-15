using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantityText;
    public Button button;

    private InventorySlot slot;
    private Inventory inventory;

    // =========================
    // SETUP SLOT
    // =========================
    public void Setup(InventorySlot newSlot, Inventory inv)
    {
        slot = newSlot;
        inventory = inv;

        if (slot == null || slot.item == null)
        {
            Clear();
            return;
        }

        icon.sprite = slot.item.icon;
        icon.enabled = true;

        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }
    // =========================
    // CLICK EVENT
    // =========================
    void OnClick()
    {
        Debug.Log($"Slot clicked! Item: {(slot != null && slot.item != null ? slot.item.itemName : "NULL")}");

        if (slot == null)
        {
            Debug.LogError("Slot is null!");
            return;
        }

        if (slot.item == null)
        {
            Debug.LogError("Item is null!");
            return;
        }

        if (inventory == null)
        {
            Debug.LogError("Inventory is null!");
            return;
        }

        inventory.UseItem(slot);
    }
    // =========================
    // CLEAR SLOT
    // =========================
    public void Clear()
    {
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }
}
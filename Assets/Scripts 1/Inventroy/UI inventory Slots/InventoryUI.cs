using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    private List<InventorySlotUI> uiSlots = new List<InventorySlotUI>();

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        Debug.Log("RefreshUI called");

        if (slotPrefab == null || slotParent == null)
        {
            Debug.LogError("SlotPrefab or SlotParent is missing!");
            return;
        }

        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        uiSlots.Clear();

        foreach (InventorySlot slot in inventory.slots)
        {
            if (slot.item == null) continue;

            GameObject obj = Instantiate(slotPrefab, slotParent);

            InventorySlotUI ui = obj.GetComponent<InventorySlotUI>();

            if (ui != null)
            {
                ui.Setup(slot, inventory); 
                uiSlots.Add(ui);
            }
        }
    }
}
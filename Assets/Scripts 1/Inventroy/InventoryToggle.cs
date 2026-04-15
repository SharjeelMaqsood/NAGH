using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI;

    public InputAction toggleInventory; // assign in inspector
    private bool isOpen = false;

    void OnEnable()
    {
        toggleInventory.Enable();
    }

    void OnDisable()
    {
        toggleInventory.Disable();
    }

    void Update()
    {
        if (toggleInventory.WasPressedThisFrame()) // better than IsPressed
        {
            isOpen = !isOpen;
            inventoryUI.SetActive(isOpen);

            // Cursor handling
            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
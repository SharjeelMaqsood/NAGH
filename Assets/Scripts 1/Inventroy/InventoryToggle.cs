using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventoryUI;

    [Header("Input")]
    [SerializeField] private InputAction toggleInventory;

    [Header("Reference")]
    [SerializeField] private CursorUnlocker cursorUnlocker;

    private bool isOpen = false;

    private void OnEnable()
    {
        toggleInventory.Enable();
    }

    private void OnDisable()
    {
        toggleInventory.Disable();
    }

    private void Start()
    {
        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if (!toggleInventory.WasPressedThisFrame())
            return;

        isOpen = !isOpen;
        inventoryUI.SetActive(isOpen);

        if (cursorUnlocker != null)
        {
            cursorUnlocker.ForceState(isOpen);
        }
    }
}
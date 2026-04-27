using UnityEngine;
using UnityEngine.InputSystem;

public class CursorUnlocker : MonoBehaviour
{
    [SerializeField] private InputAction toggleCursor;
    public bool isCursorUnlocked = true;

    private void OnEnable()
    {
        toggleCursor.Enable();
        ApplyState();
    }

    private void OnDisable()
    {
        toggleCursor.Disable();
    }

    private void Update()
    {
        if (toggleCursor.WasPressedThisFrame())
        {
            isCursorUnlocked = !isCursorUnlocked;
            ApplyState();
        }
    }

    public void ForceState(bool open)
    {
        isCursorUnlocked = open;
        ApplyState();
    }

    private void ApplyState()
    {
        Cursor.lockState = isCursorUnlocked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isCursorUnlocked;
    }
}
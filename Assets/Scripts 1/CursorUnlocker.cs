using UnityEngine;
using UnityEngine.InputSystem;

public class CursorUnlocker : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputAction toggleCursor;
    [SerializeField] private CameraSwitcher cameraSwitcher;

    [Header("References")]
    [SerializeField] private Controller controller;
    [SerializeField] private MouseMovement mouseMovement;

    [Header("State")]
    public bool isCursorUnlocked = false;

    private void OnEnable()
    {
        toggleCursor.Enable();
    }

    private void OnDisable()
    {
        toggleCursor.Disable();
    }

    private void Start()
    {
        ApplyState();
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
       
        if (isCursorUnlocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (controller != null)
            controller.enabled = !isCursorUnlocked;

   
        if (mouseMovement != null && cameraSwitcher != null)
        {
            bool shouldEnableMouseLook =
                cameraSwitcher.IsFirstPerson && !isCursorUnlocked;

            mouseMovement.enabled = shouldEnableMouseLook;
        }
    }
}
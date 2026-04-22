using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    private PlayerInputActions controls;
    float xRotation = 0f;
    float yRotation = 0f;
    [SerializeField] private Transform playerBody;

    public void Awake()
    {
        controls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable(); 
    }
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseInput = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

       
        yRotation += mouseX;

       
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
    public void ResetLook()
    {
        xRotation = 0f;

        // keep current yaw, don’t reset movement freedom
        if (playerBody != null)
            yRotation = playerBody.eulerAngles.y;

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
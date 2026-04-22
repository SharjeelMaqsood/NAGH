using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public bool IsFirstPerson => isFirstPerson;

    public Camera firstPersonCamera;
    public GameObject thirdPersonCamera;

    public MouseMovement mouseLookScript;

    private bool isFirstPerson = true;

    private void Start()
    {
        SetFirstPerson();
    }

    public void OnSwitchCamera(InputValue value)
    {
        if (value.isPressed)
            ToggleCamera();
    }

    void ToggleCamera()
    {
        if (isFirstPerson)
            SetThirdPerson();
        else
            SetFirstPerson();
    }

    void SetFirstPerson()
    {
        firstPersonCamera.gameObject.SetActive(true);
        thirdPersonCamera.SetActive(false);

        mouseLookScript.enabled = true;

        transform.rotation = Quaternion.identity;

        
        mouseLookScript.ResetLook();

        isFirstPerson = true;
    }

    void SetThirdPerson()
    {
        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.SetActive(true);

        mouseLookScript.enabled = false;

       
        transform.rotation = Quaternion.identity;

        thirdPersonCamera.transform.rotation = Quaternion.LookRotation(transform.forward);

        isFirstPerson = false;
    }
}
using SunTemple;
using UnityEngine;

public class GameplayInputController : MonoBehaviour
{
    [SerializeField] private CursorUnlocker cursorManager;
    [SerializeField] private Controller controller;
    [SerializeField] private MouseMovement mouseMovement;
    [SerializeField] private CameraSwitcher cameraSwitcher;

    void Update()
    {
        bool isUnlocked = cursorManager.isCursorUnlocked;

        if (controller != null)
            controller.enabled = !isUnlocked;

        if (mouseMovement != null && cameraSwitcher != null)
        {
            mouseMovement.enabled = cameraSwitcher.IsFirstPerson && !isUnlocked;
        }
    }
}
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public static DoorController Instance;

    public GameObject doorObject;

    void Awake()
    {
        Instance = this;
    }

    public void UnlockDoor()
    {
        Destroy(doorObject);
    }
}
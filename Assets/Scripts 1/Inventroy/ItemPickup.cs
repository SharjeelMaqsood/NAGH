using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    public int amount = 1;

    public void Pickup(Inventory inventory)
    {
        bool added = inventory.AddItem(item, amount);

        if (added)
        {
            Destroy(gameObject);
      

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inv = other.GetComponent<Inventory>();

            if (inv != null)
            {
                Pickup(inv);
            }
        }
    }
}
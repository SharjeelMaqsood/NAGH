using UnityEngine;

public class StoryCollectible : MonoBehaviour, IInteractable
{
    public string storyText;

    public void Interact()
    {
        StoryManager.Instance.CollectScript(storyText);
        Destroy(gameObject);
    }
}
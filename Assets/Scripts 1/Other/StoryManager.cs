using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;

    public int totalScripts = 5;
    private int collected = 0;

    public TMP_Text counterText;
    public GameObject storyPanel;
    public TMP_Text storyTextUI;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        storyPanel.SetActive(false);
    }

    public void CollectScript(string text)
    {
        collected++;
        storyPanel.SetActive(true);
        storyTextUI.text = text;
        UpdateUI();

        if (collected >= totalScripts)
        {
            DoorController.Instance.UnlockDoor();
        }
    }

    public void CloseStory()
    {
        storyPanel.SetActive(false);
    }

    void UpdateUI()
    {
        counterText.text = collected + " / " + totalScripts;
    }
}
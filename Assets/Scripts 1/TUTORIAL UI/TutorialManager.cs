using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Image tutorialImage;

    public Sprite[] tutorialSprites;

    public Button nextButton;
    public Button closeButton;

    private int currentIndex = 0;

    void Start()
    {
        ShowTutorialAtStart();
    }

    public void ShowTutorialAtStart()
    {
        tutorialPanel.SetActive(true);
        currentIndex = 0;
        UpdateTutorial();
    }

    public void OpenTutorialFromMenu()
    {
        tutorialPanel.SetActive(true);
        currentIndex = 0;
        UpdateTutorial();
    }

    public void NextTutorial()
    {
        currentIndex++;

        if (currentIndex >= tutorialSprites.Length)
        {
            CloseTutorial();
            return;
        }

        UpdateTutorial();
    }

    void UpdateTutorial()
    {
        tutorialImage.sprite = tutorialSprites[currentIndex];

        // If last image → show close button instead of next
        if (currentIndex == tutorialSprites.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
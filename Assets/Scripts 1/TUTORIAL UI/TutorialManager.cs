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

    private const string TUTORIAL_KEY = "TutorialShown";

    void Start()

    {
        PlayerPrefs.DeleteAll();
        // Only show if NOT shown before
        if (PlayerPrefs.GetInt(TUTORIAL_KEY, 0) == 0)
        {
            ShowTutorial();
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    void ShowTutorial()
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

        // Mark as shown forever
        PlayerPrefs.SetInt(TUTORIAL_KEY, 1);
        PlayerPrefs.Save();
    }
}
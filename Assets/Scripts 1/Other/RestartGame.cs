using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartGame : MonoBehaviour
{
    public float delay = 1f;

    public void Restart()
    {
        StartCoroutine(RestartWithDelay());
    }

    IEnumerator RestartWithDelay()
    {
        yield return new WaitForSecondsRealtime(delay); // works even when paused
        Time.timeScale = 1f; // reset time BEFORE loading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
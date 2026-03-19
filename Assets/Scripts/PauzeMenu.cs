using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsPanel; 

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SaveGame()
    {
        Debug.Log("Save Game clicked");

        // Put your save system here later
        // Example:
        // SaveSystem.SavePlayer();
    }

    public void OpenSettings()
    {
        Debug.Log("Settings clicked");

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VictorTest");
    }
}
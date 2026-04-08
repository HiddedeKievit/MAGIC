using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsPanel;

    [Header("Speed Mode")]
    public float normalSpeed = 1f;
    public float fastSpeed = 2f;
    public float ultraSpeed = 3f;
    public TextMeshProUGUI speedText;

    private bool isPaused = false;
    private float currentSpeed;
    private float baseFixedDeltaTime;

    void Start()
    {
        pauseMenuUI.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        baseFixedDeltaTime = Time.fixedDeltaTime;
        currentSpeed = normalSpeed;
        ApplySpeed();
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
        print("Resume Game clicked");
        pauseMenuUI.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (GameSpeedManager.Instance != null)
            Time.timeScale = GameSpeedManager.Instance.CurrentSpeed;
        else
            Time.timeScale = 1f;

        isPaused = false;
    }

    public void ToggleSpeed()
    {
        if (Mathf.Approximately(currentSpeed, normalSpeed))
        {
            currentSpeed = fastSpeed; // x2
        }
        else if (Mathf.Approximately(currentSpeed, fastSpeed))
        {
            currentSpeed = ultraSpeed; // x3
        }
        else
        {
            currentSpeed = normalSpeed; // back to x1
        }

        if (!isPaused)
            ApplySpeed();
        else
            UpdateSpeedText();
    }

    private void ApplySpeed()
    {
        Time.timeScale = currentSpeed;
        Time.fixedDeltaTime = baseFixedDeltaTime * currentSpeed;
        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = "x" + currentSpeed.ToString("0");
        }
    }

    public void SaveGame()
    {
        Debug.Log("Save Game clicked");
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
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene("VictorTest");
    }
}
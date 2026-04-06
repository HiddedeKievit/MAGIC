using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class deathscreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private HealthManager HealthManager;
    public GameObject deathScreenUI;

    
    private float currentSpeed;
    private float baseFixedDeltaTime;
        public float normalSpeed = 1f;

    void Start()
    {
        deathScreenUI.SetActive(false);


        baseFixedDeltaTime = Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (HealthManager.Health <= 0)
        {
            // Load the death screen scene
            Time.timeScale = 0f;
            deathScreenUI.SetActive(true);
        }
    }



    public void RestartGame()
    {
        print("Restart Game clicked");
        deathScreenUI.SetActive(false);

        SceneManager.LoadScene("LVL 1");
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene("VictorTest");
    }
}


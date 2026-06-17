using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadCutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void LoadBramTest()
    {
        SceneManager.LoadScene("BramTest");
    }

    public void LoadVictorTest()
    {
        SceneManager.LoadScene("VictorTest");
    }
}

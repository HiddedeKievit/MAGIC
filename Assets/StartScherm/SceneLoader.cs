using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadCutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }
}

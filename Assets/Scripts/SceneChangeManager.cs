using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChangeManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset SceneAsset;
#endif

    [SerializeField] private string SceneName;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (SceneAsset != null)
        {
            SceneName = SceneAsset.name;
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (string.IsNullOrWhiteSpace(SceneName))
        {
            Debug.LogError($"{name}: SceneName is empty. Set the target scene in the Inspector.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(SceneName))
        {
            Debug.LogError($"{name}: Scene '{SceneName}' is not in Build Settings or the name is incorrect. Add the scene to Build Settings and use its exact name.");
            return;
        }

        SceneManager.LoadScene(SceneName);
    }
}

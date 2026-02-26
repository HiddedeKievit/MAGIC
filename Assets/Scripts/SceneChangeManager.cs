using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] string SceneName;

    //Add this script on a GameObject with BoxCollider2D, enable 'Is Trigger'.
    //Then input the SceneName you want to teleport to in the Inspector.

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}

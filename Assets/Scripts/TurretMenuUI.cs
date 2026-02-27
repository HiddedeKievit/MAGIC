using UnityEngine;

public class TurretMenu : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject turretButtonPrefab;

    private const int MAX_VISIBLE = 6;

    void OnEnable()
    {
        PopulateMenu();
    }

    public void PopulateMenu()
    {
        // Clear old buttons
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (BuildManager.Instance == null)
        {
            Debug.LogError("BuildManager not found!");
            return;
        }

        var turrets = BuildManager.Instance.UnlockedTurrets;
        int count = Mathf.Min(turrets.Count, MAX_VISIBLE);

        for (int i = 0; i < count; i++)
        {
            GameObject buttonGO = Instantiate(turretButtonPrefab, transform);

            TurretButton button = buttonGO.GetComponent<TurretButton>();
            button.Setup(turrets[i]);
        }
    }
}
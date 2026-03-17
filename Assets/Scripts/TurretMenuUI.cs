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
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (BuildManager.Instance == null) return;

        var turrets = BuildManager.Instance.UnlockedTurrets;
        int count = Mathf.Min(turrets.Count, MAX_VISIBLE);

        for (int i = 0; i < count; i++)
        {
            GameObject buttonGO = Instantiate(turretButtonPrefab, transform, false);

            RectTransform rt = buttonGO.GetComponent<RectTransform>();

            rt.localScale = Vector3.one;
            rt.localRotation = Quaternion.identity;

            rt.anchorMin = new Vector2(1f, 0f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.pivot = new Vector2(1f, 0f);

            rt.sizeDelta = new Vector2(181.826f, 185.98f);

      

            TurretButton button = buttonGO.GetComponent<TurretButton>();
            button.turretData = turrets[i];
            button.placementManager = PlacementManager.Instance;
            button.Setup();
        }
        }
    }

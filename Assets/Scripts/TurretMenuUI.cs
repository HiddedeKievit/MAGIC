using UnityEngine;

public class TurretMenu : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject turretButtonPrefab;
    [SerializeField] private float startY = -10f;
    [SerializeField] private float spacing = 90f;

    private const int MAX_VISIBLE = 3;

    private void Start()
    {
        PopulateMenu();
    }

    public void PopulateMenu()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        if (BuildManager.Instance == null) return;

        var turrets = BuildManager.Instance.UnlockedTurrets;
        int count = Mathf.Min(turrets.Count, MAX_VISIBLE);

        for (int i = 0; i < count; i++)
        {
            GameObject buttonGO = Instantiate(turretButtonPrefab, transform, false);

            RectTransform rt = buttonGO.GetComponent<RectTransform>();

            rt.anchorMin = new Vector2(0.5f, 1f);
            rt.anchorMax = new Vector2(0.5f, 1f);
            rt.pivot = new Vector2(0.5f, 1f);

            rt.localScale = Vector3.one;

           

            rt.anchoredPosition = new Vector2(0f, startY - (i * spacing));

            TurretButton button = buttonGO.GetComponent<TurretButton>();
            button.turretData = turrets[i];
            button.placementManager = PlacementManager.Instance;
            button.Setup();
        }
    }
}
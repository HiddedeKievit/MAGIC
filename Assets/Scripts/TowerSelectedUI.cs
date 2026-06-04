using UnityEngine;

public class TowerSelectedUI : MonoBehaviour
{
    public static TowerSelectedUI Instance;

    [SerializeField] private GameObject panel;

    private TowerData currentTower;
    private Transform anchor;

    private Camera cam;
    private RectTransform canvasRect;
    private RectTransform panelRect;

    private void Awake()
    {
        Instance = this;

        Debug.Log("[TowerSelectedUI] Awake");

        cam = Camera.main;

        if (cam == null)
            Debug.LogError("[TowerSelectedUI] Camera.main NOT FOUND");

        if (panel == null)
        {
            Debug.LogError("[TowerSelectedUI] Panel NOT ASSIGNED");
            return;
        }

        canvasRect =
            GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        panelRect =
            panel.GetComponent<RectTransform>();

        panel.SetActive(false);

        Debug.Log("[TowerSelectedUI] Initialized successfully");
    }

    private void Update()
    {
        if (currentTower == null || anchor == null)
            return;

        Vector3 screenPos =
            cam.WorldToScreenPoint(anchor.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            cam,
            out Vector2 localPos
        );

        panelRect.localPosition = localPos;
    }

    public void Show(TowerData tower, Transform uiAnchor)
    {
        Debug.Log("[TowerSelectedUI] Show() called");

        currentTower = tower;
        anchor = uiAnchor;

        if (tower == null)
        {
            Debug.LogError("[TowerSelectedUI] Tower parameter is NULL");
        }

        if (uiAnchor == null)
        {
            Debug.LogError("[TowerSelectedUI] UI Anchor is NULL");
        }

        panel.SetActive(true);

        Debug.Log("[TowerSelectedUI] Panel enabled");
    }

    public void Hide()
    {
        Debug.Log("[TowerSelectedUI] Hide() called");

        currentTower = null;
        anchor = null;

        panel.SetActive(false);
    }

    public void Sell()
    {
        Debug.Log("[TowerSelectedUI] Sell button pressed");

        if (currentTower == null)
        {
            Debug.LogWarning("[TowerSelectedUI] No tower selected");
            return;
        }

        Debug.Log($"[TowerSelectedUI] Destroying {currentTower.name}");

        Destroy(currentTower.gameObject);

        Hide();

        SelectionManager.Instance.DeselectCurrent();
    }
}
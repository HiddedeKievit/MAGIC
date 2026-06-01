using UnityEngine;

public class TowerSelectedUI : MonoBehaviour
{
    public static TowerSelectedUI Instance;

    [SerializeField] private GameObject panel;

    private Tower currentTower;
    private Transform anchor;

    private Camera cam;
    private RectTransform canvasRect;
    private RectTransform panelRect;

    private void Awake()
    {
        Instance = this;

        cam = Camera.main;

        canvasRect =
            GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        panelRect =
            panel.GetComponent<RectTransform>();

        panel.SetActive(false);
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

    public void Show(Tower tower, Transform uiAnchor)
    {
        currentTower = tower;
        anchor = uiAnchor;

        panel.SetActive(true);
    }

    public void Hide()
    {
        currentTower = null;
        anchor = null;

        panel.SetActive(false);
    }

    public void Sell()
    {
        if (currentTower == null)
            return;

        Destroy(currentTower.gameObject);

        Hide();

        SelectionManager.Instance.DeselectCurrent();
    }
}
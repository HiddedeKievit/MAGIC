using UnityEngine;

public class TowerSelectedUI : MonoBehaviour
{
    public static TowerSelectedUI Instance;

    [SerializeField] private GameObject panel;

    private Tower currentTower;
    private Transform anchor;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (currentTower == null || anchor == null)
            return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(anchor.position);

        RectTransform canvasRect = panel.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransform panelRect = panel.GetComponent<RectTransform>();

        Vector2 localPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            Camera.main,
            out localPos
        );

        panelRect.localPosition = localPos;
    }

    public void Show(Tower tower)
    {
        currentTower = tower;
        anchor = tower.transform.Find("UIAnchor");

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
        if (currentTower != null)
        {
            Destroy(currentTower.gameObject);
            Hide();
        }
    }

    public void OnRangeUpgradePressed()
    {
        if (currentTower == null) return;

        currentTower.UpgradeRange();
    }

    public void OnDoubleGunUpgradePressed()
    {
        if (currentTower == null) return;

        currentTower.UpgradeDoubleGun();
    }
}
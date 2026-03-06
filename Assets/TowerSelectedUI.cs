using UnityEngine;

public class TowerSelectedUI : MonoBehaviour
{
    public static TowerSelectedUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private Canvas canvas;

    private Tower currentTower;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (currentTower == null)
            return;

        Vector2 screenPos =
            Camera.main.WorldToScreenPoint(currentTower.UIAnchor.position);

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        panel.GetComponent<RectTransform>().localPosition = localPoint;
    }

    public void Show(Tower tower)
    {
        currentTower = tower;
        panel.SetActive(true);
    }

    public void Hide()
    {
        currentTower = null;
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
}
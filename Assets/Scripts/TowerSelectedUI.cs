using UnityEngine;

public class TowerSelectedUI : MonoBehaviour
{
    public static TowerSelectedUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private UnityEngine.UI.Image path1ButtonIcon;
[SerializeField] private UnityEngine.UI.Image path2ButtonIcon;

[SerializeField] private Sprite path1Level1Icon;
[SerializeField] private Sprite path1Level2Icon;

[SerializeField] private Sprite path2Level1Icon;
[SerializeField] private Sprite path2Level2Icon;

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

    // Reset buttons for new tower
    path1ButtonIcon.gameObject.SetActive(true);
    path2ButtonIcon.gameObject.SetActive(true);

    UpdateUpgradeButtons();
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

void UpdateUpgradeButtons()
{
    if (currentTower == null)
        return;

    // PATH 1
    if (currentTower.Path1Level == 0)
        path1ButtonIcon.sprite = path1Level1Icon;

    else if (currentTower.Path1Level == 1)
        path1ButtonIcon.sprite = path1Level2Icon;

    else
        path1ButtonIcon.gameObject.SetActive(false);


    // PATH 2
    if (currentTower.Path2Level == 0)
        path2ButtonIcon.sprite = path2Level1Icon;

    else if (currentTower.Path2Level == 1)
        path2ButtonIcon.sprite = path2Level2Icon;

    else
        path2ButtonIcon.gameObject.SetActive(false);
}

    public void OnPath1UpgradePressed()
{
    if (currentTower == null) return;

    currentTower.UpgradePath1();
    UpdateUpgradeButtons();
}

public void OnPath2UpgradePressed()
{
    if (currentTower == null) return;

    currentTower.UpgradePath2();
    UpdateUpgradeButtons();
}
}
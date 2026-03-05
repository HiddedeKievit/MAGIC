using UnityEngine;

public class TowerSelectedUI : MonoBehaviour
{
    public static TowerSelectedUI Instance;

    [SerializeField] private GameObject panel;

    private Tower currentTower;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (currentTower == null)
            return;

        // Follow tower
        Vector3 pos = Camera.main.WorldToScreenPoint(currentTower.transform.position);
        transform.position = pos + new Vector3(0, -80f, 0);
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
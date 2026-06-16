using UnityEngine;
using TMPro;

public class BoulderSpawnerSelection : MonoBehaviour
{
    [SerializeField] private Transform uiAnchor;

    [Header("Countdown Display")]
    [SerializeField] private GameObject countdownObject;
    [SerializeField] private TMP_Text countdownText;

    public Transform UIAnchor => uiAnchor;

    public bool IsSelected { get; private set; }

    private TowerData tower;
    private BoulderSpawnerTower spawner;

    private void Awake()
    {
        tower = GetComponent<TowerData>();
        spawner = GetComponent<BoulderSpawnerTower>();

        if (countdownObject != null)
        {
            countdownObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!IsSelected || countdownText == null || spawner == null)
            return;

        countdownText.text =
            Mathf.CeilToInt(spawner.TimeRemaining).ToString();
    }

    public void Select()
    {
        IsSelected = true;

        if (countdownObject != null)
        {
            countdownObject.SetActive(true);
        }

        if (TowerSelectedUI.Instance != null)
        {
            TowerSelectedUI.Instance.Show(tower, uiAnchor);
        }
    }

    public void Deselect()
    {
        IsSelected = false;

        if (countdownObject != null)
        {
            countdownObject.SetActive(false);
        }

        if (TowerSelectedUI.Instance != null)
        {
            TowerSelectedUI.Instance.Hide();
        }
    }
}
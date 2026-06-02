using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private Transform uiAnchor;

    public Transform UIAnchor => uiAnchor;

    public bool IsSelected { get; private set; }

    private TowerData tower;

    private void Awake()
    {
        Debug.Log($"[TowerSelection] Awake on {name}");

        tower = GetComponent<TowerData>();

        if (tower == null)
        {
            Debug.LogError($"[TowerSelection] NO TowerData component found on {name}");
            return;
        }

        Debug.Log($"[TowerSelection] TowerData component found on {name}");

        if (rangeIndicator == null)
        {
            Debug.LogWarning($"[TowerSelection] Range Indicator NOT assigned on {name}");
        }
        else
        {
            // Scale the indicator to match tower range
            float diameter = tower.range * 2f;

            rangeIndicator.transform.localScale =
                new Vector3(diameter, diameter, 1f);

            rangeIndicator.SetActive(false);

            Debug.Log(
                $"[TowerSelection] Range indicator scaled to diameter {diameter}"
            );
        }

        if (uiAnchor == null)
        {
            Debug.LogWarning($"[TowerSelection] UI Anchor NOT assigned on {name}");
        }
        else
        {
            Debug.Log($"[TowerSelection] UI Anchor assigned: {uiAnchor.name}");
        }
    }

    private void LateUpdate()
    {
        // Keep range indicator from rotating if something accidentally rotates parent
        if (rangeIndicator != null)
        {
            rangeIndicator.transform.rotation = Quaternion.identity;
        }
    }

    public void Select()
    {
        Debug.Log($"[TowerSelection] Select() called on {name}");

        IsSelected = true;

        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(true);
            Debug.Log("[TowerSelection] Range indicator enabled");
        }

        if (TowerSelectedUI.Instance == null)
        {
            Debug.LogError("[TowerSelection] TowerSelectedUI.Instance is NULL");
            return;
        }

        Debug.Log("[TowerSelection] Showing UI");

        TowerSelectedUI.Instance.Show(tower, uiAnchor);
    }

    public void Deselect()
    {
        Debug.Log($"[TowerSelection] Deselect() called on {name}");

        IsSelected = false;

        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
            Debug.Log("[TowerSelection] Range indicator disabled");
        }

        if (TowerSelectedUI.Instance != null)
        {
            TowerSelectedUI.Instance.Hide();
        }
    }

    // Call this later if upgrades increase range
    public void RefreshRangeIndicator()
    {
        if (tower == null || rangeIndicator == null)
            return;

        float diameter = tower.range * 2f;

        rangeIndicator.transform.localScale =
            new Vector3(diameter, diameter, 1f);

        Debug.Log(
            $"[TowerSelection] Range indicator refreshed to diameter {diameter}"
        );
    }
}
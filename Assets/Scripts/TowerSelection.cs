using UnityEngine;
using TMPro;

public class TowerSelection : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform uiAnchor;

    [Header("Range Indicator (Optional)")]
    [SerializeField] private GameObject rangeIndicator;

    [Header("Countdown Display (Optional)")]
    [SerializeField] private GameObject countdownObject;
    [SerializeField] private TMP_Text countdownText;

    public Transform UIAnchor => uiAnchor;

    public bool IsSelected { get; private set; }

    private TowerData tower;
    private BoulderSpawnerTower spawner;

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

        // Optional spawner component
        spawner = GetComponent<BoulderSpawnerTower>();

        // Range indicator setup
        if (rangeIndicator == null)
        {
            Debug.Log($"[TowerSelection] No range indicator assigned on {name}");
        }
        else
        {
            RefreshRangeIndicator();
            rangeIndicator.SetActive(false);
        }

        // Countdown setup
        if (countdownObject != null)
        {
            countdownObject.SetActive(false);
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

    private void Update()
    {
        // Update countdown if this tower is a spawner
        if (!IsSelected)
            return;

        if (spawner != null && countdownText != null)
        {
            countdownText.text =
                Mathf.CeilToInt(spawner.TimeRemaining).ToString();
        }
    }

    private void LateUpdate()
    {
        // Keep range indicator from rotating if parent rotates
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

        if (countdownObject != null)
        {
            countdownObject.SetActive(true);
            Debug.Log("[TowerSelection] Countdown enabled");
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

        if (countdownObject != null)
        {
            countdownObject.SetActive(false);
            Debug.Log("[TowerSelection] Countdown disabled");
        }

        if (TowerSelectedUI.Instance != null)
        {
            TowerSelectedUI.Instance.Hide();
        }
    }

    /// <summary>
    /// Updates the visual range indicator to match the tower's actual range.
    /// Call this after upgrades if range changes.
    /// </summary>
    public void RefreshRangeIndicator()
    {
        if (tower == null || rangeIndicator == null)
            return;

        SpriteRenderer sr = rangeIndicator.GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError(
                $"[TowerSelection] Range Indicator on {name} has no SpriteRenderer!"
            );
            return;
        }

        if (sr.sprite == null)
        {
            Debug.LogError(
                $"[TowerSelection] Range Indicator on {name} has no sprite assigned!"
            );
            return;
        }

        float spriteWidth = sr.sprite.bounds.size.x;
        float diameter = tower.range * 2f;

        // Compensate for parent scaling
        float parentScale = transform.lossyScale.x;

        float scale = diameter / spriteWidth / parentScale;

        rangeIndicator.transform.localScale =
            new Vector3(scale, scale, 1f);

        Debug.Log(
            $"[TowerSelection] Range={tower.range} | Diameter={diameter} | SpriteWidth={spriteWidth} | ParentScale={parentScale} | FinalScale={scale}"
        );
    }
}
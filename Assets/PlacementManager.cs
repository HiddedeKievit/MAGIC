using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance { get; private set; }

    private TurretData selectedTurret;
    private GameObject ghostTurret;

    [SerializeField] private LayerMask placementLayer; // what counts as valid placement

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void StartPlacement(TurretData turret)
    {
        ClearPlacement();

        selectedTurret = turret;
        ghostTurret = Instantiate(turret.turretPrefab);
        // Make ghost semi-transparent
        var renderers = ghostTurret.GetComponentsInChildren<SpriteRenderer>();
        foreach (var r in renderers)
            r.color = new Color(r.color.r, r.color.g, r.color.b, 0.5f);
    }

    void Update()
    {
        if (ghostTurret == null) return;

        FollowMouse();

        if (Input.GetMouseButtonDown(0)) TryPlace();
        if (Input.GetMouseButtonDown(1)) CancelPlacement();
    }

    void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        ghostTurret.transform.position = mousePos;
    }

    void TryPlace()
    {
        // Simple placement check using LayerMask
        Collider2D hit = Physics2D.OverlapPoint(ghostTurret.transform.position, placementLayer);
        if (hit == null)
        {
            // Place turret
            Instantiate(selectedTurret.turretPrefab, ghostTurret.transform.position, Quaternion.identity);
            ClearPlacement();
        }
        else
        {
            Debug.Log("Cannot place here!");
        }
    }

    void CancelPlacement()
    {
        ClearPlacement();
    }

    void ClearPlacement()
    {
        if (ghostTurret != null) Destroy(ghostTurret);
        selectedTurret = null;
    }
}
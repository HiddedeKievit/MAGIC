using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance { get; private set; }

    private TurretData selectedTurret;
    private GameObject ghostTurret;

    [SerializeField] private LayerMask placementLayer; 

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartPlacement(TurretData turret)
    {
        ClearPlacement();

        selectedTurret = turret;

       
        ghostTurret = Instantiate(turret.ghostPrefab);

        
        foreach (var col in ghostTurret.GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }
    }

    void Update()
    {
        if (ghostTurret == null || selectedTurret == null)
            return;

        FollowMouse();

        if (Input.GetMouseButtonDown(0))
            TryPlace();

        if (Input.GetMouseButtonDown(1))
            CancelPlacement();
    }

    void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        ghostTurret.transform.position = mousePos;
    }

    void TryPlace()
    {
        Vector2 size = selectedTurret.FootprintSize;
        Vector3 position = ghostTurret.transform.position;

        Collider2D hit = Physics2D.OverlapBox(
            position,
            size,
            0f,
            placementLayer
        );

        if (hit != null)
        {
            Debug.Log("Placement blocked by: " + hit.name);
            return;
        }

        
        GameObject placed = Instantiate(
            selectedTurret.turretPrefab,
            position,
            Quaternion.identity
        );

        
        placed.layer = LayerMask.NameToLayer("PlacementBlocker");

        ClearPlacement();
    }

    void CancelPlacement()
    {
        ClearPlacement();
    }

    void ClearPlacement()
    {
        if (ghostTurret != null)
            Destroy(ghostTurret);

        ghostTurret = null;
        selectedTurret = null;
    }

#if UNITY_EDITOR
    
    void OnDrawGizmos()
    {
        if (ghostTurret == null || selectedTurret == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            ghostTurret.transform.position,
            selectedTurret.FootprintSize
        );
    }
#endif
}
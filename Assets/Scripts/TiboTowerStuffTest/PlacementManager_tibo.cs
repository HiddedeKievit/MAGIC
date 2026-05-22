using UnityEngine;


public class PlacementManager_tibo : MonoBehaviour
{
    public static PlacementManager_tibo Instance;

    public SpriteRenderer GhostTower;
    public TowerData TowerToPlace;
    private bool canPlace = false;

    private Color clr_cantplace = new(1f, 0.25f, 0.25f, 0.8f);
    private Color clr_canplace = new(0.25f, 1f, 0.25f, 0.8f);

    [SerializeField] private LayerMask placementLayer;


    Vector3 mousePosition = new();


    private void Awake()
    {
        Instance = this;
    }

    public void StartPlacementt(TowerData tower)
    {
        tower.enabled = false;
        TowerToPlace = tower;
        GhostTower.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        GhostTower.transform.localScale = TowerToPlace.transform.localScale;
    }
    private void Update()
    {
        // check if i have a tower to place.
        if (!TowerToPlace)
            return;

        // check if can place here. 

        canPlace =
            TowerManager.Instance.GetOverlappingTower(GhostTower.transform.position, TowerToPlace.size) == null
            && !Physics2D.OverlapBox(GhostTower.transform.position, TowerToPlace.size, 0f, placementLayer) && ManaManager.Instance.CanAfford(TowerToPlace.cost);

        if (canPlace)
        {

            GhostTower.color = clr_canplace;
            // place

            if (Input.GetMouseButtonDown(0))
            {
                // buy
                ManaManager.Instance.SpendMana(TowerToPlace.cost);

                // instantiate a new TowerToPlace at mouse location, set towertoplace and ghosttower to null
                Debug.Log("le click");

                TowerData newTower = TowerManager.Instance.SpawnTower(TowerToPlace, GhostTower.transform.position);
                newTower.enabled = true;

                GhostTower.sprite = null;
                TowerToPlace = null;

                return;
            }
        } else
        {
            // else check if spriterenderer can set to like red
            GhostTower.color = clr_cantplace;
        }

        if (Input.GetMouseButtonDown(1))
        {
            GhostTower.sprite = null;
            TowerToPlace = null;
            return;
        }

        if (GhostTower.sprite)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GhostTower.transform.position = mousePosition;
        }
    }

}
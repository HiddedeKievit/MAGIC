using UnityEngine;

public class PlacementManager_tibo : MonoBehaviour
{
    public static PlacementManager_tibo Instance;

    public SpriteRenderer GhostTower;
    public TowerData TowerToPlace;
    private bool canPlace = false;

    // the colors to use for when you can and cant place a tower.
    private Color clr_cantplace = new(1f, 0.25f, 0.25f, 0.8f);
    private Color clr_canplace = new(0.25f, 1f, 0.25f, 0.8f);

    // where the tower CANT get placed.
    [SerializeField] private LayerMask placementLayer;

    Vector3 mousePosition = new();

    private void Awake()
    {
        if (Instance)
            return;
        Instance = this;
    }

    public void StartPlacementt(TowerData tower)
    {
        // disable tower so it doesnt start shooting.
        tower.enabled = false;
        TowerToPlace = tower;
        // set ghost tower sprite and scale to match the tower we're placing.
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
            // is the tower i want to place NOT overlapping any other towers?
            TowerManager.Instance.GetOverlappingTower(GhostTower.transform.position, TowerToPlace.hitbox) == null
            // is the hitbox of the tower i want to place NOT overlapping anything on the placement layer?
            && !Physics2D.OverlapBox(GhostTower.transform.position, TowerToPlace.hitbox, 0f, placementLayer)
            // can i still afford the tower?
            && ManaManager.Instance.CanAfford(TowerToPlace.cost);

        if (canPlace)
        {
            GhostTower.color = clr_canplace;

            // click left button, spend mana and spawn new tower then reset the ghost tower.
            if (Input.GetMouseButtonDown(0))
            {
                ManaManager.Instance.SpendMana(TowerToPlace.cost);

                TowerData newTower = TowerManager.Instance.SpawnTower(TowerToPlace, GhostTower.transform.position);
                newTower.enabled = true;

                GhostTower.sprite = null;
                TowerToPlace = null;

                return;
            }

        } else
        {
            // cant be placed, make ghost tower red
            GhostTower.color = clr_cantplace;
        }

        // rightclick, cancel placement
        if (Input.GetMouseButtonDown(1))
        {
            GhostTower.sprite = null;
            TowerToPlace = null;
            return;
        }

        // if the sprite exists, move the ghost tower to mouse cursor.
        if (GhostTower.sprite)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GhostTower.transform.position = mousePosition;
        }
    }
}
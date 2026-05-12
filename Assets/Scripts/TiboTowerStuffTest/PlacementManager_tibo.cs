using UnityEngine;

public class PlacementManager_tibo : MonoBehaviour
{
    public static PlacementManager_tibo Instance;

    public SpriteRenderer GhostTower;
    public TowerData TowerToPlace;
    private bool canPlace = false;

    private Color clr_cantplace = new Color(1f, 0.5f, 0.5f, 0.75f);
    private Color clr_canplace = new Color(0.5f, 1f, 0.5f, 0.75f);

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
    }
    private void Update()
    {
        // check if i have a tower to place.
        if (!TowerToPlace)
            return;

        // check if can place here. 
        canPlace = TowerManager.Instance.GetTowerAt(GhostTower.transform.position.x, GhostTower.transform.position.y) == null;

        if (canPlace)
        {
            GhostTower.color = clr_canplace;
            // place

            if (Input.GetMouseButtonDown(0))
            {
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
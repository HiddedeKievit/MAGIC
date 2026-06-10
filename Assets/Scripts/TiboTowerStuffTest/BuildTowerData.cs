using UnityEngine;

public class BuildTowerData : MonoBehaviour
{
    // what tower does this button place
    public TowerData TowerPrefab;

    // link button to this function
    public void ButtonClicked()
    {
        // if the button doesn't have a tower linked, do noothing
        if (TowerPrefab == null)
            return;

        // check if enough mana.
        if (ManaManager.Instance.CanAfford(TowerPrefab.cost))
        {
            // start placement with the tower prefab
            PlacementManager_tibo.Instance.StartPlacementt(TowerPrefab);
        }
    }
}

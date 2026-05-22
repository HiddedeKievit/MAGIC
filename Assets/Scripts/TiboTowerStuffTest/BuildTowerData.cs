using UnityEngine;

public class BuildTowerData : MonoBehaviour
{
    public TowerData TowerPrefab;

    public void ButtonClicked()
    {
        if (TowerPrefab == null)
            return;
        if (ManaManager.Instance.CanAfford(TowerPrefab.cost))
        {
            // check if enough mana.
            PlacementManager_tibo.Instance.StartPlacementt(TowerPrefab);
        }

        // else warn?
    }
}

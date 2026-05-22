using UnityEngine;

public class BuildTowerData : MonoBehaviour
{
    public TowerData TowerPrefab;

    public void ButtonClicked()
    {
        if (TowerPrefab == null)
            return;
        // check if enough mana.
        if (ManaManager.Instance.CanAfford(TowerPrefab.cost))
        {
            PlacementManager_tibo.Instance.StartPlacementt(TowerPrefab);
        }

        // else warn?
    }
}

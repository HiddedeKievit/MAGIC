using UnityEngine;

public class BuildTowerData : MonoBehaviour
{
    public TowerData TowerPrefab;

    public void ButtonClicked()
    {
        // check if enough mana.
        PlacementManager_tibo.Instance.StartPlacementt(TowerPrefab);
    }
}

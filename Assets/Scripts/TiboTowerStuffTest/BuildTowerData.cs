using UnityEngine;

public class BuildTowerData : MonoBehaviour
{
    public TowerData TowerPrefab;

    public void ButtonClicked()
    {
        PlacementManager_tibo.Instance.StartPlacementt(TowerPrefab);
    }
}

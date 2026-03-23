using TMPro;
using UnityEngine;

public class TowerPanelManual : MonoBehaviour
{
    [SerializeField] private PlacementManager placementManager;
    [SerializeField] private TextMeshProUGUI selectedTowerText;

    public void SelectTower(TurretData turretData)
    {
        if (placementManager != null && turretData != null)
        {
            placementManager.StartPlacement(turretData);

            // 👇 Update text
            selectedTowerText.text = turretData.turretName;
        }
        else
        {
            Debug.LogWarning("Missing PlacementManager or TurretData");
        }
    }
}

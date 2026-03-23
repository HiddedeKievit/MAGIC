using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    public ManaManager manaManager;

    // Make these public so TurretMenu can assign them
    public TurretData turretData;
    public PlacementManager placementManager;

    public void Setup()
    {
    }

    public void OnClick()
{
    if (placementManager != null && turretData != null)
    {
        Debug.Log("Starting placement for: " + turretData.turretName);
        placementManager.StartPlacement(turretData);
    }
    else
        Debug.LogWarning("PlacementManager or TurretData missing on button: " + name);
}
}
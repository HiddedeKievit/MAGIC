using UnityEngine;

[CreateAssetMenu(menuName = "TD/Turret Data")]
public class TurretData : ScriptableObject
{
    public string turretName;
    public Sprite icon;
    public GameObject turretPrefab;
}
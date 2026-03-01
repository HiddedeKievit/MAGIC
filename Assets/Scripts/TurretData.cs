using UnityEngine;

[CreateAssetMenu(menuName = "TD/Turret Data")]
public class TurretData : ScriptableObject
{
    public string turretName;
    public Sprite icon;

    [Header("Prefabs")]
    public GameObject turretPrefab;
    public GameObject ghostPrefab;

    [Header("Placement")]
    [SerializeField] private Vector2 footprintSize = Vector2.one;
    public Vector2 FootprintSize => footprintSize;
}
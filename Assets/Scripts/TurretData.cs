using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense/Turret Data")]
public class TurretData : ScriptableObject
{
    [Header("UI")]
    public string turretName;
    public Sprite icon;

    [Header("Combat Stats")]
    public float range = 5f;
    public float fireRate = 1f;
    public int damage = 1;
    public float projectileSpeed = 60f;

    [Header("Prefabs")]
    public GameObject projectilePrefab;
    public GameObject turretPrefab;
    public GameObject ghostPrefab;

    [Header("Placement")]
    public Vector2 towerGrid = Vector2.one;

    [Header("Cost")]
    public float manaCost = 50f;
}
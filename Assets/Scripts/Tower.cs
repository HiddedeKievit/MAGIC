using UnityEngine;

public class Tower : MonoBehaviour
{
    public TurretData Data { get; private set; }

    [SerializeField] private Transform uiAnchor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite doubleGunSprite;

    public Transform UIAnchor => uiAnchor;

    public float CurrentRange { get; private set; }
    public float CurrentFireRate { get; private set; }
    public int CurrentDamage { get; private set; }

    public int Path1Level { get; private set; }
    public int Path2Level { get; private set; }

    public void Initialize(TurretData data)
    {
        Data = data;

        CurrentRange = data.range;
        CurrentFireRate = data.fireRate;
        CurrentDamage = data.damage;
    }

    // =========================
    // PATH 1
    // =========================

    public void UpgradePath1()
    {
        if (Path1Level >= 2) return;

        // Tier 2 lock
        if (Path1Level == 1 && Path2Level == 2)
            return;

        Path1Level++;

        if (Path1Level == 1)
        {
            UpgradeRange();
        }
        else if (Path1Level == 2)
        {
            UpgradeDamage();
        }
    }

    void UpgradeRange()
    {
        CurrentRange += 20f;

        GetComponent<TowerRangeVisualizer>()?.RefreshRange();
    }

    void UpgradeDamage()
    {
        CurrentDamage += 6;
    }

    // =========================
    // PATH 2
    // =========================

    public void UpgradePath2()
    {
        if (Path2Level >= 2) return;

        // Tier 2 lock
        if (Path2Level == 1 && Path1Level == 2)
            return;

        Path2Level++;

        if (Path2Level == 1)
        {
            UpgradeFireRate();
        }
        else if (Path2Level == 2)
        {
            UpgradeDoubleGun();
        }
    }

    void UpgradeFireRate()
    {
        CurrentFireRate *= 2f;
    }

    void UpgradeDoubleGun()
    {
        CurrentFireRate *= 2f;

        spriteRenderer.sprite = doubleGunSprite;

        Debug.Log("Double gun upgrade applied");
    }
}
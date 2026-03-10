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

    public int Path1Level { get; private set; }
    public int Path2Level { get; private set; }

    public void Initialize(TurretData data)
    {
        Data = data;

        CurrentRange = data.range;
        CurrentFireRate = data.fireRate;
    }

   public void UpgradeRange()
{
    if (Path2Level > 0) return;

    Path1Level++;

    CurrentRange += 20f;

    GetComponent<TowerRangeVisualizer>()?.RefreshRange();
}

    public void UpgradeDoubleGun()
{
    if (Path1Level > 0) return;
    Debug.Log("Double gun upgrade applied");

    Path2Level++;

    CurrentFireRate *= 2f;

    spriteRenderer.sprite = doubleGunSprite;
}



}
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Upgrade/Range")]
public class RangeUpgrade : TowerUpgrade
{
    public float bonus;

    public override void Apply(Tower tower)
    {
        tower.AddRange(bonus);
    }
}
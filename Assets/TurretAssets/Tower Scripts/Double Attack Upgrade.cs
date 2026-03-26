using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Upgrade/Double Attack")]
public class DoubleAttackUpgrade : TowerUpgrade
{
    public TowerAttackBehaviour newAttack;

    public override void Apply(Tower tower)
    {
        tower.SetAttackBehaviour(newAttack);
    }
}
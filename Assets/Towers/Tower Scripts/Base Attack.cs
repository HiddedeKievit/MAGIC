using UnityEngine;

public abstract class TowerAttackBehaviour : ScriptableObject
{
    protected Tower tower;

    public virtual void Initialize(Tower tower)
    {
        this.tower = tower;
    }

    public abstract void ExecuteAttack(Transform target);
}
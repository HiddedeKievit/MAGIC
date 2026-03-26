using UnityEngine;

public abstract class TowerUpgrade : ScriptableObject
{
    public abstract void Apply(Tower tower);
}
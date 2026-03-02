using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float range;

    public virtual void Activate() { }
}

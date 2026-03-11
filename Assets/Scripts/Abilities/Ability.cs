using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float range;
    public GameObject effectPrefab;

    public virtual void Activate(Vector3 origin) { }
}

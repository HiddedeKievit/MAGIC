using UnityEngine;

public class Tower : MonoBehaviour
{
    public TurretData Data { get; private set; }

    public void Initialize(TurretData data)
    {
        Data = data;
    }
}
using UnityEngine;
public interface ICanMove
{
    float BaseMovementSpeed { get; }
    float BaseRotateSpeed { get; }
    float CurrentSpeed { get; set; }
    float CurrentRotateSpeed { get; set; }

    Vector3 Target { get; set; }
    float TargetClearance { get; }
    bool CanMove { get; set; }
}
using UnityEngine;


[CreateAssetMenu]
public class Stun : Ability
{
    public override void Activate()
    {
        // get towers in range then stun
        Debug.Log("Stun!");

    }
}

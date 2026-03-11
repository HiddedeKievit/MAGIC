using UnityEngine;


[CreateAssetMenu]
public class TowerStun : Ability
{
    public float StunTime = 5f;
    public override void Activate(Vector3 origin)
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject entity in entities)
        {
            float distance = Vector2.Distance(origin, entity.transform.position);

            if (distance <= range)
            {
                TowerShooting shooter = entity.GetComponent<TowerShooting>();
                if (shooter)
                {

                    shooter.stunTimer = StunTime;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TowerRingOfFire : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Debug.Log("Particle collision with: " + other.name + ", tag: " + other.tag + ", num events: " + numCollisionEvents);

        if (other.CompareTag("Enemy"))
        {
            Entity entity = other.GetComponent<Entity>();
            if (entity != null)
            {
                Debug.Log("Damaging enemy: " + other.name + ", current health: " + entity.Health);
                for (int i = 0; i < numCollisionEvents; i++)
                {
                    entity.Health -= 1;
                    // Do something with the collision events
                    // For example, you could instantiate a hit effect at the collision point
                    Vector3 collisionPoint = collisionEvents[i].intersection;
                    // Instantiate(hitEffectPrefab, collisionPoint, Quaternion.identity);
                }
                Debug.Log("New health: " + entity.Health);
            }
            else
            {
                Debug.Log("No Entity component on " + other.name);
            }
        }
        else
        {
            Debug.Log("Collided with non-enemy: " + other.name);
        }
    }
}

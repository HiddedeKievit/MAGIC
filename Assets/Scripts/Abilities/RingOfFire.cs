using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Reflection;



[CreateAssetMenu]
public class RingOfFire : Ability
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  {  
    public float Damage = 10f;
    public override void Activate(Vector3 origin)
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Enemy");
    
        foreach (GameObject entity in entities)
        {
            float distance = Vector2.Distance(origin, entity.transform.position);

            if (distance <= range)
            {
                // Damage the enemy by reducing its Entity health.
                Entity enemyEntity = entity.GetComponent<Entity>();
                if (enemyEntity != null)
                {
                    enemyEntity.Health -= Mathf.CeilToInt(Damage);
                }
            }
        }
    }
  }
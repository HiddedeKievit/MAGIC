using Unity.VisualScripting;
using UnityEngine;
using System.Collections;


public class RingOfFireAttempt2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Entity entity = collision.gameObject.GetComponent<Entity>();
                if (entity != null)
                {
                    Debug.Log("Damaging enemy: " + collision.gameObject.name + ", current health: " + entity.Health);
                    entity.Health -= 1;
                    Debug.Log("New health: " + entity.Health);
                }
                else
                {
                    Debug.Log("No Entity component on " + collision.gameObject.name);
                }
            }
            else
            {
                Debug.Log("Collided with non-enemy: " + collision.gameObject.name);
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

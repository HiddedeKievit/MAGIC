using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;

    //Finds the Cinemachine confiner object.
    private void Start()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    //Checks the tag of colliding object to then swap the BoundingShape of the Cinemachine Camera if it's the player.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundry;
        }
    }

}

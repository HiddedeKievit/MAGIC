using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] BoxCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] Transform teleportTargetPosition;
    enum Direction { Up, Down, Left, Right, Teleport }

    //Finds the Cinemachine confiner object.
    private void Start()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    //Checks the tag of colliding object to then swap the BoundingShape of the Cinemachine Camera if it's the player.
    //Calls the function to push Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    //Pushes player in certain direction depending on what enum is selected.
    private void UpdatePlayerPosition(GameObject player)
    {
        if (direction == Direction.Teleport)
        {
            player.transform.position = teleportTargetPosition.position;
        }
        
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += 2;
                break;
            case Direction.Down:
                newPos.y -= 2;
                break;
            case Direction.Left:
                newPos.x -= 2;
                break;
            case Direction.Right:
                newPos.x += 2;
                break;

        }

        player.transform.position = newPos;
    }

}

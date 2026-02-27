// temp comment to fix stuff hopefully


using UnityEngine;


public class PathManager : MonoBehaviour
{

    public Transform[] checkpoints;

    // draw path

    private void OnDrawGizmos()
    {
        if (checkpoints == null || checkpoints.Length == 0)
            return;

        Gizmos.color = Color.white;

        // draw lines between checkpoints
        for (int i = 0; i < checkpoints.Length - 1; i++)
        {
            Gizmos.DrawLine(checkpoints[i].position, checkpoints[i + 1].position);
        }

        // draw points

        Gizmos.color = Color.red;

        foreach (Transform point in checkpoints)
        {
            Gizmos.DrawSphere(point.position, 0.2f);
        }
    }
}

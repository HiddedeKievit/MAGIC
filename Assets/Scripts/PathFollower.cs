// temp comment to fix stuff hopefully

using UnityEngine;


public class PathFollower : MonoBehaviour
{
    private Mover mover;
    [SerializeField] private PathManager Path;

    // what checkpoint am i at
    private int index = 0;

    void Start()
    {
        mover = GetComponent<Mover>();
        mover.Target = Path.checkpoints[index].position;
    }

    void Update()
    {
        // if theres no path, something went wrong. dont do anything.
        if (Path == null)
        {
            Debug.Log("Missing path");
            enabled = false;
            return;
        }

        // keep updating the target.
        mover.Target = Path.checkpoints[index].position;

        float distanceToTarget = Vector3.Distance(transform.position, mover.Target);

        // if close to the target
        if (distanceToTarget < mover.TargetClearence)
        {
            // if i have a path, ill now be following that path

            if (Path != null)
            {
                // last checkpoint. remove self?
                if (index + 1 >= Path.checkpoints.Length)
                {
                    enabled = false; // done

                    GetComponent<IFinishable>().ReachEnd();
                    return;
                }


                // next checkpoint
                if (index + 1 < Path.checkpoints.Length)
                    index++;
            }
        }
    }
}

using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private ICanMove mover;
    private IPathable pathSource;


    // what checkpoint am i at
    private int index = 0;

    void Start()
    {
        mover = GetComponent<ICanMove>();
        pathSource = GetComponent<IPathable>();

    }

    void Update()
    {
        // if theres no path, something went wrong. dont do anything.
        if (pathSource == null || pathSource.Path == null)
        {
            Debug.Log("Missing path source or path manager");
            enabled = false;
            return;
        }
        // keep updating the target.
        mover.Target = pathSource.Path.checkpoints[index].position;

        float distanceToTarget = Vector3.Distance(transform.position, mover.Target);

        // if close to the target
        if (distanceToTarget < mover.TargetClearance)
        {
            // if i have a path, ill now be following that path

            if (pathSource.Path != null)
            {
                // last checkpoint. remove self?
                if (index + 1 >= pathSource.Path.checkpoints.Length)
                {
                    GetComponent<IFinishable>().ReachEnd();
                    return;
                }


                // next checkpoint
                if (index + 1 < pathSource.Path.checkpoints.Length)
                    index++;
            }
        }
    }
}

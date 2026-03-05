// temp comment to fix stuff hopefully

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

        // close to the target
        if (mover.DistanceToTarget < mover.TargetClearence)
        {
            // if i have a path, ill now be following that path

            // at the last checkpoint. 
            if (index + 1 >= Path.checkpoints.Length)
            {
                // nothing to path so disable self.
                enabled = false;

                // try to get tje finishable component and activate the reach end
                if (TryGetComponent<IFinishable>(out var canFinish))
                    canFinish.ReachEnd();

                return;
            }


            // go to the next checkpoint
            if (index + 1 < Path.checkpoints.Length)
            {
                index++;
                mover.Target = Path.checkpoints[index].position;
            }
        }
    }
}

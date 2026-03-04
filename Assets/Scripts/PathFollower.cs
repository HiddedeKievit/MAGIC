
using UnityEngine;


public class PathFollower : MonoBehaviour
{
    private Mover mover;
    [SerializeField] public PathManager Path;

    // what checkpoint am i at
    private int index = 0;

    void Awake()
    {
        mover = GetComponent<Mover>();
        if (Path != null)
        {
            mover.Target = Path.checkpoints[0].position;
        }
    }



    void Update()
    {
        // if theres no path, something went wrong. dont do anything.
        if (Path == null)
            return;


        // keep updating the target.
        mover.Target = Path.checkpoints[index].position;



        // if close to the target
        if (mover.DistanceToTarget < mover.TargetClearence)
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
            {
                index++;
                mover.Target = Path.checkpoints[index].position;
            }
        }

    }
}

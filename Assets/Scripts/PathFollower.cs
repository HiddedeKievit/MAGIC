using UnityEngine;


public class PathFollower : MonoBehaviour
{
    private Entity entity;
    [SerializeField] public PathManager Path;

    // what checkpoint am i at
    private int index = 0;

    void Awake()
    {
        entity = GetComponent<Entity>();
        if (Path != null)
        {
            entity.Target = Path.checkpoints[0].position;
        }
    }



    void Update()
    {
        // if theres no path, something went wrong. dont do anything.
        if (Path == null)
            return;


        // keep updating the target.
        entity.Target = Path.checkpoints[index].position;



        // close to the target
        float DistanceToTarget = Vector2.Distance(transform.position, entity.Target);

        if (DistanceToTarget < entity.TargetClearance)
        {

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
                entity.Target = Path.checkpoints[index].position;
            }
        }

    }
}

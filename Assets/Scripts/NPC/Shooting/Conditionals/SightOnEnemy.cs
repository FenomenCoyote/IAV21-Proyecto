using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SightOnEnemy : Conditional
{
	public SharedTransform threateningEnemy;
    [SerializeField] float sightDistance = 8f;


	public override TaskStatus OnUpdate()
	{
        //Compare to myself
        RaycastHit info;
        if (Physics.Raycast(transform.position, transform.forward, out info, sightDistance))
        {
            return (info.transform.gameObject.transform == threateningEnemy.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
	}
}
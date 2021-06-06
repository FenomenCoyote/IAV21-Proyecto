using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GainSightOnEnemy : Action
{
	public SharedTransform threateningEnemy;

	NPCController controller;

	public override void OnAwake()
	{
		base.OnAwake();
		controller = GetComponent<NPCController>();
	}

	public override TaskStatus OnUpdate()
	{
		controller.lookAt(threateningEnemy.Value.position);
		return TaskStatus.Failure;
	}
}
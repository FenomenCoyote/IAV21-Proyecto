using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AimLookForEnemies : Action
{
	NPCController controller;

	Transform player;

	[SerializeField] float updateTime = 3f;
	float lastUpdateTime;

	[SerializeField] float minAngleDiff = 30f, targetMinDistance = 6f, targetMaxDistance = 14f;

    public override void OnAwake()
	{
		base.OnAwake();
		player = GameObject.Find("Player").transform;
		controller = GetComponent<NPCController>();
		lastUpdateTime = 0.0f;
	}

    public override TaskStatus OnUpdate()
	{
		if(lastUpdateTime > 0f)
        {
			lastUpdateTime -= Time.deltaTime;
			
			return TaskStatus.Success;
		}
		else
        {
			lastUpdateTime = updateTime;

			controller.changeWeapon("Shotgun");
			controller.lookAt(getNewTarget());

			return TaskStatus.Success;
        }
	}
	
	private Vector3 getNewTarget()
    {
		Vector3 dir = Quaternion.AngleAxis(Random.Range(minAngleDiff, 360f - minAngleDiff), Vector3.up) * player.forward;
		return transform.position + dir * Random.Range(targetMinDistance, targetMaxDistance);
    }
}
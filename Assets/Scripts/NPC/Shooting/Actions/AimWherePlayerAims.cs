using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AimWherePlayerAims : Action
{
    Transform player;
    NPCController controller;

    public override void OnAwake()
    {
        base.OnAwake();
        player = GameObject.Find("Player").transform;
        controller = GetComponent<NPCController>();
    }

    public override TaskStatus OnUpdate()
	{
        controller.lookAt(player.position + player.forward * 6);
		return TaskStatus.Success;
	}
}
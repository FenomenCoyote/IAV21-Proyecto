using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

/// <summary>
/// AI related
/// 
/// Called from the shooting behaviour tree
/// 
/// </summary>
public class ShootAtEnemy : Action
{
	NPCController controller;

    public override void OnAwake()
    {
        base.OnAwake();
        controller = GetComponent<NPCController>();
    }

    public override void OnStart()
    {
        base.OnStart();
        controller.stop();
    }

    public override TaskStatus OnUpdate()
	{
        controller.changeWeapon("Rifle");
        controller.shoot();
		return TaskStatus.Success;
	}
}
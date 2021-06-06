using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerReloading : Conditional
{
    PlayerWeaponController pController;

    public override void OnAwake()
    {
        base.OnAwake();
        pController = GameObject.Find("Player").GetComponentInChildren<PlayerWeaponController>();
    }

    public override TaskStatus OnUpdate()
	{
		return pController.reloading() ? TaskStatus.Success : TaskStatus.Failure;
	}
}

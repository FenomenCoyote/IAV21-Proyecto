using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Reload : Action
{
	NPCController controller;
	[SerializeField] float reloadThreshold = 0.51f;

    public override void OnAwake()
    {
        base.OnAwake();

		controller = GetComponent<NPCController>();
    }

    public override TaskStatus OnUpdate()
	{
		controller.npcWeaponController.stop();
		if (controller.npcWeaponController.wc.getWeaponPercentage() < reloadThreshold)
			controller.reload();

		return TaskStatus.Success;
	}
}

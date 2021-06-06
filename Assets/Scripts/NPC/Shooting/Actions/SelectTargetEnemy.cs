using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;

public class SelectTargetEnemy : Action
{
	private Transform enemiesPool;

	private DangerMovement dangerMovement;

	public SharedTransform threateningEnemy;

	[SerializeField] float threateningDistance = 8f;

	public override void OnAwake()
	{
		enemiesPool = GameObject.Find("EnemiesPool").transform;
		dangerMovement = GetComponent<DangerMovement>();
	}

	public override TaskStatus OnUpdate()
	{
		//If the enemy was eliminated, or it is too far away, or it is no longer visible, we need to search for another enemy
        if (threateningEnemy.Value == null || 
			Vector3.Distance(transform.position, threateningEnemy.Value.position) > threateningDistance || 
			!threateningEnemy.Value.GetComponent<EnemyLife>().touchedByLight)
		{
			return searchNewEnemy();
		}

		return TaskStatus.Success;
	}

	private TaskStatus searchNewEnemy()
    {
		List<EnemyLife> eLifes = new List<EnemyLife>();

		foreach (EnemyLife eL in enemiesPool.GetComponentsInChildren<EnemyLife>())
		{
			if (eL.touchedByLight && Vector3.Distance(transform.position, eL.transform.position) < threateningDistance)
            {
				eLifes.Add(eL);
            }
		}

		if (eLifes.Count == 0)
			return TaskStatus.Failure;

		float closestAngle = 180f;
		EnemyLife bestEnemy = null;
		//Find the one closest to my forward 
		foreach(EnemyLife eL in eLifes)
        {
			Vector3 dir = eL.transform.position - transform.position;
			float angle = Vector3.Angle(transform.forward, dir);
			if (Mathf.Abs(angle) < Mathf.Abs(closestAngle))
            {
				closestAngle = angle;
				bestEnemy = eL;
            }
        }

		if(bestEnemy == null)
        {
			dangerMovement.enemy = null;
			dangerMovement.enemyExists = false;
			return TaskStatus.Failure;
        }
        else
        {
			threateningEnemy.Value = bestEnemy.transform;
			dangerMovement.enemy = bestEnemy.transform;
			dangerMovement.enemyExists = true;
		}


		return TaskStatus.Success;
	}
}
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Danger : Conditional
{
    private Transform enemiesPool;
    private Transform nexusPool;

    private Transform playerPos;

    [SerializeField] bool successOnDanger = true;
    [SerializeField] float dangerMinDistance = 8f;

    public override void OnStart()
    {
        base.OnStart();
        //weapons = gameObject.GetComponentsInChildren<Disparar>(true);
        enemiesPool = GameObject.Find("EnemiesPool").transform;
        nexusPool = GameObject.Find("Nexus").transform;
        playerPos = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// There is danger when an enemy or a nexus are close
    /// </summary>
    /// <returns></returns>
    public override TaskStatus OnUpdate()
	{
        if (enemyClose())
            return (successOnDanger) ? TaskStatus.Success : TaskStatus.Failure;

        return (!successOnDanger) ? TaskStatus.Success : TaskStatus.Failure;
    }

    private bool enemyClose()
    {
        Transform[] enemies = enemiesPool.GetComponentsInChildren<Transform>();

        //Skip the first one because it's always the parent
        for(int i = 1; i < enemies.Length; ++i) {
            //If one enemy is close, there is danger 
            if(closeToAnyOfUs(enemies[i].position, dangerMinDistance)) {
                return true;
            }
        }

        return false;
    }

    private bool closeToAnyOfUs(Vector3 enemyPos, float distance)
    {
        //Compare to myself
        Vector3 dir = enemyPos - transform.position;
        RaycastHit info;
        if (Physics.Raycast(transform.position, dir, out info, distance)) {
            EnemyLife eL = info.transform.gameObject.GetComponent<EnemyLife>();
            if(eL != null && eL.touchedByLight)
                return true;
        }

        //Compare to the player
        dir = enemyPos - playerPos.position;
        if (Physics.Raycast(playerPos.position, dir, out info, distance)) {
            EnemyLife eL = info.transform.gameObject.GetComponent<EnemyLife>();
            if (eL != null && eL.touchedByLight)
                return true;
        }

        return false;
    }
}
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

/// <summary>
/// AI related
/// 
/// Called from the shooting behaviour tree
/// 
/// </summary>
public class Danger : Conditional
{
    private Transform enemiesPool;
    private Transform nexusPool;

    private Transform playerPos;

    NPCController controller;
    DangerMovement dangerMovement;

    [SerializeField] bool successOnDanger = true;
    [SerializeField] float dangerMinDistance = 8f;

    public override void OnAwake()
    {
        base.OnAwake();
        //weapons = gameObject.GetComponentsInChildren<Disparar>(true);
        enemiesPool = GameObject.Find("EnemiesPool").transform;
        nexusPool = GameObject.Find("Nexus").transform;
        playerPos = GameObject.Find("Player").transform;
        controller = GetComponent<NPCController>();
        dangerMovement = GetComponent<DangerMovement>();
    }

    /// <summary>
    /// There is danger when an enemy or a nexus are close
    /// </summary>
    /// <returns></returns>
    public override TaskStatus OnUpdate()
	{
        bool danger = enemyClose() || nexusClose();
        controller.inDanger = danger;
        dangerMovement.enemyExists = danger;

        if (danger)
        {
            return (successOnDanger) ? TaskStatus.Success : TaskStatus.Failure;
        }


        return (!successOnDanger) ? TaskStatus.Success : TaskStatus.Failure;
    }

    private bool enemyClose()
    {
        EnemyLife[] enemies = enemiesPool.GetComponentsInChildren<EnemyLife>();

        for(int i = 0; i < enemies.Length; ++i) {
            //If one enemy is close, there is danger 
            if(closeToAnyOfUs(enemies[i].transform.position, dangerMinDistance)) {
                return true;
            }
        }

        return false;
    }

    private bool nexusClose()
    {
        NexusSpawner[] nexuses = nexusPool.GetComponentsInChildren<NexusSpawner>();

        for (int i = 0; i < nexuses.Length; ++i)
        {
            //If one enemy is close, there is danger 
            if (closeToAnyOfUs(nexuses[i].transform.position, dangerMinDistance, "Nexus"))
            {
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

    private bool closeToAnyOfUs(Vector3 enemyPos, float distance, string tag)
    {
        //Compare to myself
        Vector3 dir = enemyPos - transform.position;
        RaycastHit info;
        if (Physics.Raycast(transform.position, dir, out info, distance) && info.transform.gameObject.CompareTag(tag))
        {
            return true;
        }

        //Compare to the player
        dir = enemyPos - playerPos.position;
        if (Physics.Raycast(playerPos.position, dir, out info, distance) && info.transform.gameObject.CompareTag(tag))
        {
            return true;
        }

        return false;
    }
}
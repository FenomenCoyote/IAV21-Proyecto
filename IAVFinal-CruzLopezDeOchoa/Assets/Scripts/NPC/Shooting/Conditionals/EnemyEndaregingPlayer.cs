using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

/// <summary>
/// AI related
/// 
/// Called from the shooting behaviour tree
/// 
/// </summary>
public class EnemyEndaregingPlayer : Conditional
{
    private Transform enemiesPool;

    private Transform playerPos;

    private DangerMovement dangerMovement;

    [SerializeField] float dangerMinDistance = 2f;

    public SharedTransform threateningEnemy;

    public override void OnAwake()
    {
        base.OnAwake();
        //weapons = gameObject.GetComponentsInChildren<Disparar>(true);
        enemiesPool = GameObject.Find("EnemiesPool").transform;
        playerPos = GameObject.Find("Player").transform;
        dangerMovement = GetComponent<DangerMovement>();
    }

    /// <summary>
    /// There is threatening danger to the player when an enemy is very close to him
    /// </summary>
    /// <returns></returns>
    public override TaskStatus OnUpdate()
    {
        return enemyClose() ? TaskStatus.Success : TaskStatus.Failure;
    }

    private bool enemyClose()
    {
        EnemyLife[] enemies = enemiesPool.GetComponentsInChildren<EnemyLife>();

        //Skip the first one because it's always the parent
        for (int i = 0; i < enemies.Length; ++i)
        {
            //If one enemy is close, there is danger 
            if (closeToPlayer(enemies[i].transform.position))
            {
                return true;
            }
        }

        return false;
    }


    private bool closeToPlayer(Vector3 enemyPos)
    {
        Vector3 dir = enemyPos - playerPos.position;
        RaycastHit info;
        if (Physics.Raycast(playerPos.position, dir, out info, dangerMinDistance))
        {
            EnemyLife eL = info.transform.gameObject.GetComponent<EnemyLife>();
            if (eL != null && eL.touchedByLight)
            {
                threateningEnemy.Value = eL.transform;
                dangerMovement.enemy = eL.transform;
                dangerMovement.enemyExists = true;
                return true;
            }
        }

        threateningEnemy.Value = null;
        dangerMovement.enemy = null;
        dangerMovement.enemyExists = false;

        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Game related
/// Basic enemy ai that follow the player if it's in range
/// </summary>
public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] float distanceToFollow = 10, timeToDespawn = 20;

    NavMeshAgent nav;
    Transform player;

    float timeWaiting = 0;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange())
        {
            nav.isStopped = false;
            nav.SetDestination(player.position);
        }
        else
        {
            nav.isStopped = true;
            timeWaiting += Time.deltaTime;
            if (timeWaiting > timeToDespawn) Destroy(gameObject);
        }
    }

    bool playerInRange()
    {
        return (transform.position - player.position).magnitude < distanceToFollow;
    }

    public void setDistanceToFollow(float distance) { distanceToFollow = distance; }
}

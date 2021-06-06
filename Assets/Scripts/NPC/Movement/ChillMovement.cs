using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChillMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 offset = Vector3.zero;

    [SerializeField] float patrolDistance = 5f;
    [SerializeField] float patrolAngle = 30f;

    public void calculateNewOffset()
    {
        Vector3 inverseDir = transform.position - player.position;
        offset = inverseDir.normalized + new Vector3(Random.Range(-0.25f, 0.25f), 0, Random.Range(-0.25f, 0.25f));
    }

    public Vector3 calculateArrivePos()
    {
        return player.position + offset;
    }

    public Vector3 calculateAroundPos()
    {
        Vector3 dir = player.forward * -1f * patrolDistance;
        dir = Quaternion.AngleAxis(Random.Range(-patrolAngle, patrolAngle), Vector3.up) * dir;
        return transform.position + dir;
    }
}

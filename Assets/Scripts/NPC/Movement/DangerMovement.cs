using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    public Transform enemy { private get; set; }

    public bool enemyExists = false;

    [SerializeField] float groupUpMaxDistance = 2.5f;
    [SerializeField] float noAngle_angleMovement = 45f;
    [SerializeField] float bothering_angleMovement = 60f;
    [SerializeField] float bothering_angle = 15f;

    public Vector3 gainAnglePos()
    {
        if (!enemyExists || enemy == null || !ReferenceEquals(enemy, null))
            return transform.position;

        Vector3 dir = player.position - transform.position;

        float anglePlayer = Vector3.Angle(dir.normalized, Vector3.right);
        float angleEnemy = Vector3.Angle(enemy.position - transform.position, Vector3.right);

        float angleMovement = (anglePlayer < angleEnemy) ? -noAngle_angleMovement : noAngle_angleMovement;

        return transform.position + Quaternion.AngleAxis(angleMovement, Vector3.up) * dir;
    }

    public Vector3 calculateGroupUpPos()
    {
        Vector3 dir = player.forward * groupUpMaxDistance;
        dir = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * dir;
        return player.position + dir;
    }

    public Vector3 calculateDontBotherPos()
    {
        Vector3 dir = player.position - transform.position;

        return transform.position + Quaternion.AngleAxis(bothering_angleMovement, Vector3.up) * dir;
    }

    public bool noAngle()
    {
        if (!enemyExists || enemy == null || !ReferenceEquals(enemy, null)) return false;
        else {
            RaycastHit info;
            return !(Physics.Raycast(transform.position, enemy.position, out info, 12f) && info.transform == enemy.transform);
        }
    }

    public bool botheringPlayer()
    {
        Vector3 dir = player.position - transform.position;

        float anglePlayer = Mathf.Abs(Vector3.Angle(dir.normalized, player.forward));

        return anglePlayer < bothering_angle;
    }
}

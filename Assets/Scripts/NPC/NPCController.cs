using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent agent;

    public NPCWeaponController npcWeaponController { get; private set; }

    Vector3 _direction;
    Quaternion _lookRotation;

    Vector3 lookAtTarget;

    public bool inDanger { get; set; }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        npcWeaponController = gameObject.GetComponentInChildren<NPCWeaponController>();
        lookAtTarget = Vector3.forward;
        inDanger = false;
    }

    private void Update()
    {
        //find the vector pointing from our position to the target
        _direction = (lookAtTarget - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    public void goToPos(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void lookAt(Vector3 target)
    {
        lookAtTarget = target;
    }

    public void shoot()
    {
        npcWeaponController.shoot();
    }

    public void stop()
    {
        npcWeaponController.stop();
    }

    public void reload()
    {
        npcWeaponController.reload();
    }

    public void changeWeapon()
    {
        npcWeaponController.changeWeapon();
    }

    public void changeWeapon(string weapon)
    {
        npcWeaponController.changeWeapon(weapon);
    }
}

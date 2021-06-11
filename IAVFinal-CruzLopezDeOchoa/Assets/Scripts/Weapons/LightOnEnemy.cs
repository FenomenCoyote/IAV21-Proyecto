using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI related
/// Tells the enemy that it is being touched by a light
/// </summary>
public class LightOnEnemy : MonoBehaviour
{
    [SerializeField] float forgetTime = 2f;

    private void OnTriggerStay(Collider other)
    {
        EnemyLife eL = other.gameObject.GetComponent<EnemyLife>();
        if (eL != null)
        {
            eL.touchedByLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyLife eL = other.gameObject.GetComponent<EnemyLife>();
        if (eL != null)
        {
            StartCoroutine(forgetEnemy(eL));
        }
    }

    IEnumerator forgetEnemy(EnemyLife eL)
    {
        yield return new WaitForSeconds(forgetTime);
        eL.touchedByLight = false;
    }
}

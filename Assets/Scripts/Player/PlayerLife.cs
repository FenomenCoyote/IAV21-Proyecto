using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float life { get; private set; } = 10.0f;
    [SerializeField] bool imPlayer = false;

    public bool makeDamage(float damage)
    {
        life -= damage;
        if (life <= 0.98) {
            if (imPlayer)
                UnityEditor.EditorApplication.isPlaying = false;
            else
                Destroy(this.gameObject);
        }
        return life <= 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        EnemyAttack eA = collision.collider.GetComponent<EnemyAttack>();
        if (eA && eA.canAttack()) {
            makeDamage(eA.getDamage());
            eA.justAttacked();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        EnemyAttack eA = collision.collider.GetComponent<EnemyAttack>();
        if (eA)
        {
            eA.lowerCd();
        }
    }
}

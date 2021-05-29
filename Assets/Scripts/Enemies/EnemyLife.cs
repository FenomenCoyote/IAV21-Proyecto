using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    [SerializeField] float vida = 10f;

    public float getLife() { return vida; }

    public bool damageEnemy(float damage)
    {
        vida -= damage;
        if (vida <= 0) Destroy(gameObject);
        return vida <= 0;
    }
}

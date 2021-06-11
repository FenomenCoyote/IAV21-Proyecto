using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 2f;
    [SerializeField] float cd = 2;
    float leftCd;

    private void Start()
    {
        leftCd = 0;
    }

    private void Update()
    {
        leftCd -= Time.deltaTime;
    }

    public bool canAttack() { return leftCd <= 0; }
    public float getDamage() { return damage; }
    public void justAttacked() { leftCd = cd; }
    public void lowerCd() {
        leftCd -= cd / 2;
    }

}

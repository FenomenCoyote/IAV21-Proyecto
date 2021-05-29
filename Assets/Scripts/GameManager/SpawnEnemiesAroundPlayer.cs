using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesAroundPlayer : MonoBehaviour
{
    [SerializeField] EnemyFollowPlayer[] enemyTypes;
    [SerializeField] float cd, chancePerCd;
    [SerializeField] int numPerCd, randomnessOfNum;
    [SerializeField] float rangeToSpawn;

    Transform player, enemiesPool;
    float leftCd;

    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.Find("Player").transform;
        enemiesPool = GameObject.Find("EnemiesPool").transform;
        leftCd = cd;
    }

    // Update is called once per frame
    void Update()
    {
        leftCd -= Time.deltaTime;
        if(leftCd <= 0)
        {
            leftCd = cd;
            if (Random.Range(0, 1) < chancePerCd)
                spawnEnemies(numPerCd + Random.Range(-randomnessOfNum, randomnessOfNum));
        }
    }

    void spawnEnemies(int num)
    {
        for(int i = 0; i < num; ++i)
        {
            EnemyFollowPlayer enemy = getRandomEnemy();
            Instantiate(enemy, getRandomPos(enemy), Quaternion.identity, enemiesPool).setDistanceToFollow(rangeToSpawn + 5);
        }
    }

    EnemyFollowPlayer getRandomEnemy()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Length)];
    }

    Vector3 getRandomPos(EnemyFollowPlayer enemy)
    {
        Vector2 randomDir, randomPos;
        Vector3 pos;
        int tries = -1;
        do {
            tries++;
            randomDir.x = Random.Range(-100, 100);
            randomDir.y = Random.Range(-100, 100);
            randomDir.Normalize();
            randomPos = randomDir * rangeToSpawn;
            pos = new Vector3(randomPos.x, 0, randomPos.y) + player.position;
        } while (!validPos(pos, enemy) && tries < 10);
      
        return pos;
    }

    bool validPos(Vector3 pos, EnemyFollowPlayer enemy)
    {
        Collider c = enemy.gameObject.GetComponent<Collider>();
        pos.y = c.bounds.extents.y + 0.01f;
        return pos.x > -49 && pos.x < 49 && pos.z > -49 && pos.z < 49 && !Physics.CheckBox(pos, c.bounds.extents);
    }
}

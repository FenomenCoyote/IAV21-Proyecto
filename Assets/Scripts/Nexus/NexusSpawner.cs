using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusSpawner : MonoBehaviour
{
    [SerializeField] EnemyFollowPlayer enemy;
    [SerializeField] float cd, randomness, spawnRange, safeRange, rangeToSpawn;
    Transform spawnPoint, pool, player;
    float leftCd;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.GetChild(0);
        pool = GameObject.Find("EnemiesPool").transform;
        player = GameObject.Find("Player").transform;
        leftCd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        leftCd -= Time.deltaTime;
        if (leftCd < 0)
        {
            leftCd = cd + Random.Range(-randomness, randomness);
            float distance = (transform.position - player.position).magnitude;
            if (distance < rangeToSpawn && distance > safeRange)
                spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemy, getRandomPos(enemy), Quaternion.identity, pool).setDistanceToFollow(rangeToSpawn + 5);
    }

    Vector3 getRandomPos(EnemyFollowPlayer enemy)
    {
        Vector2 randomDir, randomPos;
        Vector3 pos;
        int tries = -1;
        do
        {
            tries++;
            randomDir.x = Random.Range(-100, 100);
            randomDir.y = Random.Range(-100, 100);
            randomDir.Normalize();
            randomPos = randomDir * spawnRange;
            pos = new Vector3(randomPos.x, 0, randomPos.y) + spawnPoint.position;
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


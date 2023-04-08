using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    //public int NumberOfEnemiesToSpawn = 5;
    public float spawnDelay = 1f;

    private EnemyPool enemyPool;
    private MediumEnemyPool mediumEnemyPool;
    private HardEnemyPool hardEnemyPool;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyPool = GameObject.Find("EnemyPool").GetComponent<EnemyPool>();
        mediumEnemyPool = GameObject.Find("MediumEnemyPool").GetComponent<MediumEnemyPool>();
        hardEnemyPool = GameObject.Find("HardEnemyPool").GetComponent<HardEnemyPool>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) StopAllCoroutines();

        else if (player.GetComponent<PlayerController>().dead == true)
            StopAllCoroutines();
    }

    public void startSpawn()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void FixedUpdate()
    {

    }

    private IEnumerator SpawnEnemies()
    {
        int rand = Random.Range(1, 20);
        if (rand <= 10)
        {
            spawnDelay = 0.6f;
            enemyPool.getEnemyPool();
        }

        else if (rand > 10 && rand <= 16)
        {
            spawnDelay = 0.6f;
            mediumEnemyPool.getEnemyPool();
        }

        else if (rand > 16)
        {
            spawnDelay = 0.6f;
            hardEnemyPool.getEnemyPool();
        }

        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnEnemies());
    }
}

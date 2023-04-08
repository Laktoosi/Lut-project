using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HardEnemyPool : MonoBehaviour
{
    [SerializeField] private int minAmount = 100;
    [SerializeField] private int maxAmount = 500;

    [SerializeField] private float spawnRadius = 5f;

    #region
    [SerializeField] private Enemy enemy;


    [SerializeField] private bool usePool;
    private ObjectPool<Enemy> enemyPool;
    #endregion

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, minAmount, maxAmount);
    }

    #region
    private Enemy CreatePooledObject()
    {
        Enemy instance = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(Enemy instance)
    {
        enemyPool.Release(instance);
    }

    private void OnTakeFromPool(Enemy instance)
    {
        instance.gameObject.SetActive(true);
        SpawnEnemy(instance);
        instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(Enemy instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Enemy instance)
    {
        Destroy(instance.gameObject);
    }

    public void SpawnEnemy(Enemy instance)
    {
        Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        instance.transform.position = spawnPos;
    }

    public void getEnemyPool()
    {
        enemyPool.Get();
    }
    #endregion


}

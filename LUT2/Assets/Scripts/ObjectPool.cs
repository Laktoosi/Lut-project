using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int minAmount = 100;
    [SerializeField] private int maxAmount = 500;

    #region
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Bullet bullet;


    [SerializeField] private bool usePool;
    private ObjectPool<Bullet> bulletPool;

    [SerializeField] public Transform gunPoint;
    #endregion

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, minAmount, maxAmount);
    }

    #region
    private Bullet CreatePooledObject()
    {
        Bullet instance = Instantiate(bullet, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(Bullet instance)
    {
        bulletPool.Release(instance);
    }

    private void OnTakeFromPool(Bullet instance)
    {
        instance.gameObject.SetActive(true);
        SpawnBullet(instance);
        instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(Bullet instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Bullet instance)
    {
        Destroy(instance.gameObject);
    }

    public void SpawnBullet(Bullet instance)
    {
        float rand = Random.Range(-5f, 5f);
        instance.transform.position = gunPoint.position;
        instance.transform.rotation = Quaternion.Euler(new Vector3(gunPoint.transform.eulerAngles.x, gunPoint.transform.eulerAngles.y, gunPoint.transform.eulerAngles.z + rand));
    }

    public void getBulletPool()
    {
        bulletPool.Get();
    }
    #endregion
}
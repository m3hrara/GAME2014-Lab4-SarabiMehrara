using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public Queue<GameObject> bulletPool;
    public int bulletNumber;
    private BulletFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>(); // creates an empty Queue
        factory = GetComponent<BulletFactory>(); // gets a reference to the bullet factory
        //BuildBulletPool();
    }

    /// <summary>
    ///  This method builds a bullet pool of bulletNumber bullets
    /// </summary>
    private void BuildBulletPool()
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            AddBullet();
        }
    }

    private void AddBullet()
    {
        var temp_bullet = factory.createBullet();
        bulletPool.Enqueue(temp_bullet);
    }

    /// <summary>
    /// This method removes a bullet prefab from the bullet pool
    /// and returns a reference to it.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public GameObject GetBullet(Vector2 spawnPosition)
    {
        if (bulletPool.Count < 1)
        {
            AddBullet();
            bulletNumber++;
        }

        var temp_bullet = bulletPool.Dequeue();
        temp_bullet.transform.position = spawnPosition;
        temp_bullet.SetActive(true);
        return temp_bullet;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        bulletPool.Enqueue(returnedBullet);
    }
}

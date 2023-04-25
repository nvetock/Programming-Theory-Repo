using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{

    /*public float poolSize;
    public GameObject projectilePrefab;
    private List<Bullet> projectilesInPool;
    public static ProjectilePool Instance;
    */
    public static ProjectilePool Instance;
    private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    void Awake()
    {
        //Instance = GetComponent<ProjectilePool>();
        Instance = this;
    }

    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        GameObject _projectile = pooledObjects[0];
        _projectile.transform.position = position;
        _projectile.transform.rotation = rotation;
        pooledObjects.Remove(_projectile);

        return _projectile;
    }



    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    /*
    void Start()
    {
        InitializePool();
    }


    public void ReturnToPool(Bullet _projectile)
    {
        _projectile.transform.position = transform.position;
        projectilesInPool.Add(_projectile);
    }

    void InitializePool()
    {
        projectilesInPool = new List<Bullet>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject _projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectilesInPool.Add(_projectile.GetComponent<Bullet>());
        }
    }
    */
}

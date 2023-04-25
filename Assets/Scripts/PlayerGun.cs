using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    Transform firingPoint; //find where the shot will fire from

    [SerializeField]
    float firingSpeed; //how fast is THIS fired

    public static PlayerGun Instance;

    private float lastTimeShot = 0;


    void Awake()
    {
        Instance = GetComponent<PlayerGun>();
    }

    public void Shoot()
    {
            if (lastTimeShot + firingSpeed <= Time.time)
            {
                GameObject pooledProjectile = ProjectilePool.Instance.GetPooledObject();
                if (pooledProjectile != null)
                {
                //pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = transform.position;
            }
            pooledProjectile.GetComponent<Bullet>().Move();
            lastTimeShot = Time.time;

        }
    }
    }

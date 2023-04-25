using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 firingPoint;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float maxProjectileDistance;
    public float damage = 1.0f;

    public bool shouldMove = false;


    void Start()
    {
        firingPoint = GameObject.Find("PlayerGun").transform.position;    
    }

    void Update()
    {
        if (shouldMove)
        {
            MoveProjectile();
        }
    }

    public void Move()
    {
        shouldMove = true;
    }

    void MoveProjectile()
    {
        
        if(Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            //ProjectilePool.Instance.ReturnToPool(this);
            shouldMove = false;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        other.gameObject.GetComponent<Enemy>().DamageIntake(damage);
        //Destroy(gameObject);
    }

}

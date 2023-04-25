using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    //Health Logic Variables
    [SerializeField] private float maxHealth;
    [SerializeField] private int pointValue;
    [SerializeField] private float curHealth;
    public bool isHit = false;



    //Walking Logic Variables
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotSpeed = 100f;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        HandleWanderAI();

        if(curHealth <= 0f)
        {
            GameObject.Find("Canvas").GetComponent<MainUIHandler>().AddScore(pointValue);
            Destroy(gameObject);
            Debug.Log(gameObject.name + " is Dead!");
        }
    }

    public float DamageIntake(float damage)
    {
        if (damage > 0f)
        {
            curHealth -= damage;
        }

        return curHealth;
    }


    private void HandleWanderAI()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;

    }


    /*
    private void OnTriggerEnter(Collider other)
    {

        if (CompareTag("Bullet"))
        {
            damage = GameObject.FindGameObjectWithTag("Bullet").GetComponent<Bullet>().damage;
            DamageIntake(damage);
        }
    }*/
}

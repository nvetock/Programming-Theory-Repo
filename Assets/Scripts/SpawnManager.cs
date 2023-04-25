using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject[] pickupPrefab;

    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnPosZ;
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float animalSpawnInterval;
    [SerializeField] private float pickupSpawnInterval;


    private void Start()
    {
        animalSpawnInterval = Random.Range(3, 6);
        pickupSpawnInterval = Random.Range(0, 4);

        InvokeRepeating("SpawnRandomAnimal", startDelay, animalSpawnInterval);
        InvokeRepeating("SpawnPickups", startDelay, pickupSpawnInterval);
    }

    void SpawnRandomAnimal()
    {
        //int animalIndex = Random.Range(0, 3); 3 is used here as an exclusive number, only 0-2 is selected, but 3 is seen as the maximum

        //using the array variable from above, the range length can be called like this for scalability
        int enemyIndex = Random.Range(0, enemyPrefab.Length);
        spawnRangeX = Random.Range(1, 51f);
        spawnPosZ = Random.Range(1, 51f);
        //below is a variable created to randomize vector3 positioning
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(enemyPrefab[enemyIndex], spawnPos, enemyPrefab[enemyIndex].transform.rotation);
    }

    void SpawnPickups()
    {
        int pickupIndex = 0;
        spawnRangeX = Random.Range(1, 51f);
        spawnPosZ = Random.Range(1, 51f);
        //below is a variable created to randomize vector3 positioning
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.375f, spawnPosZ);

        Instantiate(pickupPrefab[pickupIndex], spawnPos, pickupPrefab[pickupIndex].transform.rotation);
    }
}

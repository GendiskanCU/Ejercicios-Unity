using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;
    
    private float spawnInterval = 1.0f;

    private int indexBall = 0;


    private void Start()
    {
        Invoke("SpawnRandomBall", spawnInterval);
    }

   

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
        
        //Se elige aleatoriamente la bola que va a spawnear
        indexBall = Random.Range(0, ballPrefabs.Length);

        // instantiate ball at random spawn location
        Debug.Log("Nueva bola lanzada después de " + spawnInterval.ToString("N2") + " segundos");
        Instantiate(ballPrefabs[indexBall], spawnPos, ballPrefabs[indexBall].transform.rotation);
        
        spawnInterval = Random.Range(2.0f, 4.0f);
        Invoke("SpawnRandomBall", spawnInterval);
    }

}

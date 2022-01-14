using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2f;
    public float spawnInterval = 2f;

    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnObjects", spawnDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    private void Update()
    {
        //Incrementa la dificultad reduciendo el intervalo de aparición de objetos en escena al pasar el tiempo
        if (spawnInterval > 0.5f)
            spawnInterval -= Time.deltaTime / 20;
        
    }

    // Spawn obstacles
    void SpawnObjects ()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
        
        Invoke("SpawnObjects", spawnInterval);
    }
}

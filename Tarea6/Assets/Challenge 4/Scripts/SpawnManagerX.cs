using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 18;
    private float goalPlayerPosition = 6;//Para que los enemigos no puedan spawnear justo frente a la portería
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    private int enemyCount;
    private int numberOfEnemies = 1;
    
    private int _waveCount = 1;

    public int WaveCount => _waveCount;

    public GameObject resetPosition;//Para colocar al player nuevamente en su posición/rotación iniciales

    public GameObject player;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip soundSpawn;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(numberOfEnemies);
            if (numberOfEnemies < 10)
            {
                numberOfEnemies++;
            }
        }

    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = goalPlayerPosition;
        while (xPos >= -goalPlayerPosition && xPos <= goalPlayerPosition)
        {
            xPos = Random.Range(-spawnRangeX, spawnRangeX);
        }

        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end
        
        //spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        _audioSource.PlayOneShot(soundSpawn, 1);
        
        _waveCount++;
            
        ResetPlayerPosition(); // put player back at start
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = resetPosition.transform.position;
        player.transform.Find("Focal Point").transform.rotation = resetPosition.transform.rotation;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}

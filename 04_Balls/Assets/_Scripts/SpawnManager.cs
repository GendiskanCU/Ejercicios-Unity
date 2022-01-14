using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    
    public int enemyWave = 1;//Número de oleada de enemigos
    

    private float spawnRange = 9f;//Rango de spawneo de los enemigos

    private int enemyCount;//Número de enemigos en la escena
    
    // Start is called before the first frame update
    void Start()
    {
        //Instanciamos una primera oleada de enemigos en posición aleatoria dentro de la zona de juego
        SpawnEnemyWave(enemyWave);
    }


    private void Update()
    {
        //Actualizamos el número de enemigos en la escena
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        //enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; Forma alternativa de hacerlo
        if (enemyCount == 0) //Cuando no queden enemigos
        {
            enemyWave++;//Se aumenta el número para la próxima oleada
            SpawnEnemyWave(enemyWave);//Se hace un nuevo spawn de enemigos y de powerUp adicionales
            int numberOfPowerUps = Random.Range(1, 3);//Entre 1 y 2
            for(int i = 0; i < numberOfPowerUps; i++)
                Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Generamos una posición aleatoria de spawn dentro de los límites de la zona de juego
    /// </summary>
    /// <returns>Devuelve una posición aleatoria dentro de la zona de juego</returns>
   private Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX, spawnPositionZ;
        
        spawnPositionX = Random.Range(-spawnRange, spawnRange);
        spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPositionX, 0, spawnPositionZ);

        return randomPos;
    }

    /// <summary>
    /// "Spawnea" una oleada de enemigos
    /// </summary>
    /// <param name="numberOfEnemies">Número de enemigos de la oleada</param>
    private void SpawnEnemyWave(int numberOfEnemies) 
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}

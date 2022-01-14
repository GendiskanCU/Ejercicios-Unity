using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject [] obstaclePrefabs;

    private Vector3 spawnPos;

    private float startDelay = 2.0f;
    private float repeatDelay = 2.0f;

    private PlayerController _playerController;//Para capturar el script que controla Player
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        spawnPos = transform.position;//Los obstáculos spawnearán en la posición donde esté colocado el Spawn Manager
        
       
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
    }

    void SpawnObstacle()
    {
        if (!_playerController.GameOver)//Si no se ha alcanzado el fin de la partida
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
